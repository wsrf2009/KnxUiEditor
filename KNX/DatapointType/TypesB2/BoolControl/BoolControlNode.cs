using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.BoolControl
{
    class BoolControlNode : TypesB2Node
    {
        public BoolControlNode()
        {
            this.KNXSubNumber = DPST_2;
            this.DPTName = "boolean control";
        }

        public static TreeNode GetTypeNode()
        {
            BoolControlNode nodeType = new BoolControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
