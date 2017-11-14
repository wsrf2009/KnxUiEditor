using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.EnableControl
{
    class EnableControlNode:TypesB2Node
    {
        public EnableControlNode()
        {
            this.KNXSubNumber = DPST_3;
            this.DPTName = "enable control";
        }

        public static TreeNode GetTypeNode()
        {
            EnableControlNode nodeType = new EnableControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
