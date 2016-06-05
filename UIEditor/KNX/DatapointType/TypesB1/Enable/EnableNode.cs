using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Enable
{
    class EnableNode : TypesB1Node
    {
        public EnableNode()
        {
            //this.Text = "1.003 enable";
            this.KNXSubNumber = DPST_3;
            this.Name = "enable";
        }

        public static TreeNode GetTypeNode()
        {
            EnableNode nodeType = new EnableNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
