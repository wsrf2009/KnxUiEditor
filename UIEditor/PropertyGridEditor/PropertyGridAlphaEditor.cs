using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using UIEditor.Component;
using UIEditor.Entity;
using System.Drawing;
using Utils;

namespace UIEditor.PropertyGridEditor
{
    public class PropertyGridAlphaEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                ViewNode vNode = context.Instance as ViewNode;
                if (null == vNode)
                {
                    return null;
                }

                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    TrackBar bar = new TrackBar();
                    bar.Maximum = 10;
                    bar.Minimum = 0;
                    bar.Value = (int)(vNode.Alpha * 10);
                    edSvc.DropDownControl(bar);

                    float alpha = (float)bar.Value / 10;
                    return (float)Math.Round(alpha, 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return value;
            }

            return value;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            base.PaintValue(e);

            ViewNode vNode = e.Context.Instance as ViewNode;
            if (null == vNode)
            {
                return;
            }

            try
            {
                Color backColor = Color.FromArgb((int)(vNode.Alpha * 255), vNode.BackgroundColor);
                Image img = ImageHelper.CreateImage(backColor);
                e.Graphics.DrawImage(img, new Rectangle(1, 1, 20, 14));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
