using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.BinaryValueControl
{
    class BinaryValueControlNode:TypesB2Node
    {
        public BinaryValueControlNode()
        {
            this.KNXSubNumber = DPST_6;
            this.Name = "binary value control";
        }

        public static TreeNode GetTypeNode()
        {
            BinaryValueControlNode nodeType = new BinaryValueControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
