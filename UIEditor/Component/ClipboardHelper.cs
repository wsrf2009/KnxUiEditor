using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Component
{
    class ClipboardHelper
    {
        public static void CopyTreeNodeToClipboard(object obj)
        {
            DataFormats.Format format = DataFormats.GetFormat(obj.GetType().FullName);
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, obj);
            Clipboard.SetDataObject(dataObj, false);
            //Clipboard.SetDataObject(obj);
        }

        public static Object GetTreeNodeFromClipboard()
        {
            TreeNode node = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(TreeNode).FullName;
            if (dataObj.GetDataPresent(format))
            {
                node = dataObj.GetData(format) as TreeNode;
            }
            return node;
            //return Clipboard.GetDataObject();
        }
    }
}
