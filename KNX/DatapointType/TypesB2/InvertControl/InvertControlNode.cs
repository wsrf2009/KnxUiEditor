using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.InvertControl
{
    class InvertControlNode:TypesB2Node
    {
        public InvertControlNode()
        {
            this.KNXSubNumber = DPST_12;
            this.DPTName = "invert control";
        }

        public static TreeNode GetTypeNode()
        {
            InvertControlNode nodeType = new InvertControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
