using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type2NibbleSet.DoubleNibble
{
    class DoubleNibbleNode:Type2NibbleSetNode
    {
        public DoubleNibbleNode()
        {
            this.KNXSubNumber = DPST_1000;
            this.DPTName = "busy/nak repetitions";
        }

        public static TreeNode GetTypeNode()
        {
            DoubleNibbleNode nodeType = new DoubleNibbleNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
