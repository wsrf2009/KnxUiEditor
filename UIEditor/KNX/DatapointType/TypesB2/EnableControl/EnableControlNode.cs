using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.EnableControl
{
    class EnableControlNode:TypesB2Node
    {
        public EnableControlNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "enable control";
        }

        public static TreeNode GetTypeNode()
        {
            EnableControlNode nodeType = new EnableControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
