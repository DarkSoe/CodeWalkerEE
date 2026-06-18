using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace CodeWalker.Utils
{
    public static class ContentThumbnailCache
    {
        public static readonly Color BackgroundColor = Color.FromArgb(0x55, 0x8B, 0xAD);

        public static string GetCacheDirectory()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "thumbnails");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        public static string GetThumbnailPath(string cleanName)
        {
            return Path.Combine(GetCacheDirectory(), cleanName + ".jpg");
        }

        public static bool Exists(string cleanName)
        {
            return File.Exists(GetThumbnailPath(cleanName));
        }

        public static Bitmap TryLoad(string cleanName)
        {
            var path = GetThumbnailPath(cleanName);
            if (!File.Exists(path))
                return null;

            try
            {
                using (var img = Image.FromFile(path))
                {
                    return new Bitmap(img);
                }
            }
            catch
            {
                return null;
            }
        }

        public static void Save(Bitmap bitmap, string cleanName, long quality = 85L)
        {
            if (bitmap == null || string.IsNullOrEmpty(cleanName))
                return;

            var path = GetThumbnailPath(cleanName);
            var codec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

            if (codec != null)
            {
                using (var encoder = new EncoderParameters(1))
                {
                    encoder.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                    bitmap.Save(path, codec, encoder);
                }
            }
            else
            {
                bitmap.Save(path, ImageFormat.Jpeg);
            }
        }

        public static Bitmap NormalizeBackground(Bitmap source)
        {
            if (source == null)
                return null;

            var result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            int bgR = 0, bgG = 0, bgB = 0;
            var samples = new[]
            {
                source.GetPixel(0, 0),
                source.GetPixel(source.Width - 1, 0),
                source.GetPixel(0, source.Height - 1),
                source.GetPixel(source.Width - 1, source.Height - 1)
            };
            foreach (var sample in samples)
            {
                bgR += sample.R;
                bgG += sample.G;
                bgB += sample.B;
            }
            bgR /= samples.Length;
            bgG /= samples.Length;
            bgB /= samples.Length;

            if (IsCloseToBackgroundColor(bgR, bgG, bgB))
                return (Bitmap)source.Clone();

            const int threshold = 48;

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var pixel = source.GetPixel(x, y);
                    if (Math.Abs(pixel.R - bgR) <= threshold &&
                        Math.Abs(pixel.G - bgG) <= threshold &&
                        Math.Abs(pixel.B - bgB) <= threshold)
                    {
                        result.SetPixel(x, y, BackgroundColor);
                    }
                    else
                    {
                        result.SetPixel(x, y, pixel);
                    }
                }
            }

            return result;
        }

        private static bool IsCloseToBackgroundColor(int r, int g, int b)
        {
            return Math.Abs(r - BackgroundColor.R) <= 20 &&
                   Math.Abs(g - BackgroundColor.G) <= 20 &&
                   Math.Abs(b - BackgroundColor.B) <= 20;
        }
    }
}
