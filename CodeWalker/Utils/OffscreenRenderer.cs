using CodeWalker.Rendering;
using CodeWalker.World;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker.Utils
{
    public class OffscreenRenderer : DXForm
    {
        public Renderer Renderer = null;

        public OffscreenRenderer()
        {
        }

        public Form Form => throw new NotImplementedException();

        public void BuffersResized(int w, int h)
        {
            throw new NotImplementedException();
        }

        public void CleanupScene()
        {
            throw new NotImplementedException();
        }

        public bool ConfirmQuit()
        {
            throw new NotImplementedException();
        }

        public void InitScene(Device device)
        {
            int width = 350;
            int height = 350;

            try
            {
                Renderer.DeviceCreated(device, width, height);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shaders!\n" + ex.ToString());
                return;
            }
        }

        public void RenderScene(DeviceContext context)
        {
            throw new NotImplementedException();
        }
    }
}
