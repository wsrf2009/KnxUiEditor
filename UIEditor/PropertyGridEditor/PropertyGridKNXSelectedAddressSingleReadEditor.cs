
using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.PropertyGridEditor
{
    class PropertyGridKNXSelectedAddressSingleReadEditor:UITypeEditor
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
                    string title = (string)InstanceHelper.GetPropertyValue("Title", context.Instance, context.Instance.GetType());
                    Dictionary<string, KNXSelectedAddress> readAddressId = (Dictionary<string, KNXSelectedAddress>)InstanceHelper.GetPropertyValue("ReadAddressId", context.Instance, context.Instance.GetType());
                    if (null != readAddressId)
                    {
                        var frm = new FrmGroupAddressPick();
                        frm.Text = UIResMang.GetString("PropEtsReadAddressId") + " - " + title;
                        frm.MultiSelect = false;
                        frm.PickType = FrmGroupAddressPick.AddressType.Read;
                        frm.SelectedAddress = readAddressId;
                        var result = frm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            //value = frm.SelectedAddress;
                            value = new Dictionary<string, KNXSelectedAddress>(frm.SelectedAddress);
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
    }
}
