using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CodeWalker.Utils
{
    public class ContentThumbnailService : IDisposable
    {
        private readonly Panel renderHostPanel;
        private readonly OffscreenRenderer renderer;
        private readonly Queue<ThumbnailRequest> queue = new Queue<ThumbnailRequest>();
        private readonly Dictionary<string, Bitmap> cache = new Dictionary<string, Bitmap>(StringComparer.OrdinalIgnoreCase);
        private readonly object syncRoot = new object();
        private readonly System.Windows.Forms.Timer readyTimer;

        private ThumbnailRequest currentRequest;
        private bool processing;
        private int requestGeneration;

        private struct ThumbnailRequest
        {
            public ContentPropItem Prop;
            public Action<Bitmap> Callback;
        }

        public ContentThumbnailService(Control host)
        {
            ContentThumbnailCache.GetCacheDirectory();

            renderHostPanel = new Panel
            {
                Size = new Size(256, 256),
                Location = new Point(-1000, -1000),
                Visible = true,
                BackColor = ContentThumbnailCache.BackgroundColor
            };

            renderer = new OffscreenRenderer();
            renderer.SaveThumbnailToDisk = false;
            renderer.TopLevel = false;
            renderer.FormBorderStyle = FormBorderStyle.None;
            renderer.Dock = DockStyle.Fill;

            renderHostPanel.Controls.Add(renderer);
            host.Controls.Add(renderHostPanel);
            renderHostPanel.SendToBack();

            renderer.Show();
            renderer.ThumbnailReady += OnThumbnailReady;

            readyTimer = new System.Windows.Forms.Timer { Interval = 100 };
            readyTimer.Tick += ReadyTimer_Tick;
            readyTimer.Start();
        }

        private void ReadyTimer_Tick(object sender, EventArgs e)
        {
            if (!renderer.IsRendererReady)
                return;

            readyTimer.Stop();
            lock (syncRoot)
            {
                TryProcessNext();
            }
        }

        public void RequestThumbnail(ContentPropItem prop, Action<Bitmap> callback)
        {
            if (prop == null || callback == null)
                return;

            var key = prop.GetCleanName();

            lock (syncRoot)
            {
                if (cache.TryGetValue(key, out var cached))
                {
                    callback(cached);
                    return;
                }
            }

            var diskCached = ContentThumbnailCache.TryLoad(key);
            if (diskCached != null)
            {
                lock (syncRoot)
                {
                    if (!cache.ContainsKey(key))
                        cache[key] = diskCached;
                    else
                        diskCached.Dispose();
                    callback(cache[key]);
                }
                return;
            }

            lock (syncRoot)
            {
                queue.Enqueue(new ThumbnailRequest { Prop = prop, Callback = callback });
                if (renderer.IsRendererReady)
                    TryProcessNext();
            }
        }

        public void ClearQueue()
        {
            lock (syncRoot)
            {
                requestGeneration++;
                queue.Clear();
                processing = false;
                currentRequest = default;
            }
        }

        private void TryProcessNext()
        {
            if (processing || !renderer.IsRendererReady)
                return;

            ThumbnailRequest request;
            int generation;
            lock (syncRoot)
            {
                if (queue.Count == 0)
                    return;

                request = queue.Dequeue();
                processing = true;
                currentRequest = request;
                generation = requestGeneration;
            }

            void startRender()
            {
                if (generation != requestGeneration)
                {
                    lock (syncRoot)
                    {
                        processing = false;
                        currentRequest = default;
                        TryProcessNext();
                    }
                    return;
                }

                renderer.ViewModel(request.Prop);
            }

            if (renderer.InvokeRequired)
                renderer.BeginInvoke((Action)startRender);
            else
                startRender();
        }

        private void OnThumbnailReady(Bitmap bitmap)
        {
            ThumbnailRequest request;
            int generation;
            lock (syncRoot)
            {
                request = currentRequest;
                generation = requestGeneration;
                processing = false;
                currentRequest = default;
            }

            if (generation != requestGeneration || request.Callback == null)
            {
                bitmap?.Dispose();
                lock (syncRoot)
                {
                    TryProcessNext();
                }
                return;
            }

            Bitmap result = null;
            if (bitmap != null && request.Prop != null)
            {
                var key = request.Prop.GetCleanName();
                using (var normalized = ContentThumbnailCache.NormalizeBackground(bitmap))
                {
                    ContentThumbnailCache.Save(normalized, key);

                    lock (syncRoot)
                    {
                        if (!cache.TryGetValue(key, out result))
                        {
                            cache[key] = new Bitmap(normalized);
                            result = cache[key];
                        }
                    }
                }
                bitmap.Dispose();
            }
            else
            {
                bitmap?.Dispose();
            }

            try
            {
                request.Callback?.Invoke(result);
            }
            catch { }

            lock (syncRoot)
            {
                TryProcessNext();
            }
        }

        public void Dispose()
        {
            readyTimer?.Stop();
            readyTimer?.Dispose();
            renderer.ThumbnailReady -= OnThumbnailReady;

            lock (syncRoot)
            {
                queue.Clear();
                foreach (var bmp in cache.Values)
                    bmp?.Dispose();
                cache.Clear();
            }

            if (renderer != null && !renderer.IsDisposed)
            {
                renderer.Hide();
                renderer.Dispose();
            }

            renderHostPanel?.Dispose();
        }
    }
}
