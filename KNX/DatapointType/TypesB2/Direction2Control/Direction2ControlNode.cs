using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.Direction2Control
{
    class Direction2ControlNode:TypesB2Node
    {
        public Direction2ControlNode()
        {
            this.KNXSubNumber = DPST_9;
            this.DPTName = "direction control 2";
        }

        public static TreeNode GetTypeNode()
        {
            Direction2ControlNode nodeType = new Direction2ControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
