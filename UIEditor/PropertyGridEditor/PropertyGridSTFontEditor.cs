using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.UserClass;

namespace UIEditor.PropertyGridEditor
{
    public class PropertyGridSTFontEditor : UITypeEditor
    {
        //public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        //{
        //    return UITypeEditorEditStyle.DropDown;
        //}

        //public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //{
        //    try
        //    {
        //        //ViewNode vNode = context.Instance as ViewNode;
        //        //if (null == vNode)
        //        //{
        //        //    return null;
        //        //}

        //        IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        //        Console.WriteLine("context:"+context+" provider:"+provider+" value:"+value+" edsvc:"+edSvc);
        //        //if (edSvc != null)
        //        //{
        //        //    TrackBar bar = new TrackBar();
        //        //    bar.Maximum = 10;
        //        //    bar.Minimum = 0;
        //        //    bar.Value = (int)(vNode.Alpha * 10);
        //        //    edSvc.DropDownControl(bar);

        //        //    double alpha = (double)bar.Value / 10;
        //        //    return Math.Round(alpha, 1);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return value;
        //    }

        //    return value;
        //}

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            base.PaintValue(e);

            try
            {
                STFont f = e.Value as STFont;
                Color fontColor = f.Color;
                Font font = new Font("宋体", 9, f.GetFontStyle());
                e.Graphics.DrawString("ab", font, new SolidBrush(fontColor), e.Bounds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PropertyGridImageEditor Error:" + ex.Message);
            }
        }
    }
}
