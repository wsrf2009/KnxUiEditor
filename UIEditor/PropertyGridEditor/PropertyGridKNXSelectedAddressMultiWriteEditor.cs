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
    class PropertyGridKNXSelectedAddressMultiWriteEditor : UITypeEditor
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
                    //ControlBaseNode node = context.Instance as ControlBaseNode;
                    //Type type = context.Instance.GetType();
                    //Console.WriteLine(type);
                    //type.GetMember("WriteAddressIds", Dictionary<string, KNXSelectedAddress>, System.Reflection.BindingFlags.);

                    string title = (string)InstanceHelper.GetPropertyValue("Title", context.Instance, context.Instance.GetType());
                    Dictionary<string, KNXSelectedAddress> writeAddressIds = 
                        (Dictionary<string, KNXSelectedAddress>)InstanceHelper.GetPropertyValue("WriteAddressIds",
                        context.Instance, context.Instance.GetType());
                    if (null != writeAddressIds)
                    {
                        var frm = new FrmGroupAddressPick();
                        frm.Text = UIResMang.GetString("PropEtsWriteAddressIds") + " - " + title;
                        frm.MultiSelect = true;
                        frm.PickType = FrmGroupAddressPick.AddressType.Write;
                        frm.SelectedAddress = writeAddressIds;
                        var result = frm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            //node.WriteAddressIds = frm.SelectedAddress;
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
