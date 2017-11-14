using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using UIEditor.UserClass;
using UIEditor.Entity;
using UIEditor.Component;

namespace UIEditor.PropertyGridEditor
{
    public class PropertyGridMultiLineTextEditor : UITypeEditor
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
                    string str = value as string;
                    if (null != str)
                    {
                        RichTextBox box = new RichTextBox();
                        box.Text = str;
                        edSvc.DropDownControl(box);

                        return box.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("PropertyGridRichText Error : " + ex.Message);
                return value;
            }
            return value;
        }

    }
}
