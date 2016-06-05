using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.OpenClose
{
    class OpenCloseNode : TypesB1Node
    {
        public OpenCloseNode()
        {
            //this.Text = "1.009 open/close";
            this.KNXSubNumber = DPST_9;
            this.Name = "open/close";
        }

        public static TreeNode GetTypeNode()
        {
            OpenCloseNode nodeType = new OpenCloseNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
