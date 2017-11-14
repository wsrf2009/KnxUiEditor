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
using System.Drawing;
using Utils;

namespace UIEditor.PropertyGridEditor
{
    public class PropertyGridImageEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = MyConst.PicFilter;
                    ofd.InitialDirectory = MyCache.ProjectResImgDir;
                    if (DialogResult.OK == ofd.ShowDialog())
                    {
                        if (null != ofd.FileName)
                        {
                            value = ImageHelper.GetDiskImage(ofd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("PropertyGridImageEditor Error : " + ex.Message);
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

            try
            {
                Image img = e.Value as Image;
                e.Graphics.DrawImage(img, new Rectangle(1, 1, 20, 14));
            }
            catch (Exception ex)
            {
                Console.WriteLine("PropertyGridImageEditor Error:" + ex.Message);
            }
        }
    }
}
