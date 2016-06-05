using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.Direction1Control
{
    class Direction1ControlNode:TypesB2Node
    {
        public Direction1ControlNode()
        {
            this.KNXSubNumber = DPST_8;
            this.Name = "direction control 1";
        }

        public static TreeNode GetTypeNode()
        {
            Direction1ControlNode nodeType = new Direction1ControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
