using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Type2NibbleSet.DoubleNibble;

namespace UIEditor.KNX.DatapointType.Type2NibbleSet
{
    class Type2NibbleSetNode:DatapointType
    {
        public Type2NibbleSetNode()
        {
            this.KNXMainNumber = DPT_25;
            this.Name = "2-nibble set";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type2NibbleSetNode nodeType = new Type2NibbleSetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(DoubleNibbleNode.GetTypeNode());

            return nodeType;
        }
    }
}
