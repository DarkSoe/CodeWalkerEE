using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CodeWalker.Rendering.UI
{
    public class DarkGreyMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkGreyMenuRenderer() : base(new DarkColors()) { }

        private class DarkColors : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Color.FromArgb(36, 36, 36);
            public override Color MenuStripGradientEnd => Color.FromArgb(36, 36, 36);

            public override Color ToolStripDropDownBackground => Color.FromArgb(36, 36, 36);

            public override Color MenuItemSelected => Color.FromArgb(60, 60, 60);
            public override Color MenuItemBorder => Color.FromArgb(100, 100, 100);

            public override Color MenuItemPressedGradientBegin => Color.FromArgb(60, 60, 60);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(60, 60, 60);

            public override Color ImageMarginGradientBegin => Color.FromArgb(36, 36, 36);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(36, 36, 36);
            public override Color ImageMarginGradientEnd => Color.FromArgb(36, 36, 36);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Owner is MenuStrip)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(21, 21, 21)), e.Item.ContentRectangle);
                }
            }
            else
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(36, 36, 36)), e.Item.ContentRectangle);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = SystemColors.Control;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
    }



    public class LightGreyMenuRenderer : ToolStripProfessionalRenderer
    {
        public LightGreyMenuRenderer() : base(new DarkColors()) { }

        private class DarkColors : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Color.FromArgb(36, 36, 36);
            public override Color MenuStripGradientEnd => Color.FromArgb(36, 36, 36);

            public override Color ToolStripDropDownBackground => Color.FromArgb(36, 36, 36);

            public override Color MenuItemSelected => Color.FromArgb(60, 60, 60);
            public override Color MenuItemBorder => Color.FromArgb(100, 100, 100);

            public override Color MenuItemPressedGradientBegin => Color.FromArgb(60, 60, 60);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(60, 60, 60);

            public override Color ImageMarginGradientBegin => Color.FromArgb(36, 36, 36);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(36, 36, 36);
            public override Color ImageMarginGradientEnd => Color.FromArgb(36, 36, 36);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Owner is MenuStrip)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(36, 36, 36)), e.Item.ContentRectangle);
                }
            }
            else
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(36, 36, 36)), e.Item.ContentRectangle);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = SystemColors.Control;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
    }
}
