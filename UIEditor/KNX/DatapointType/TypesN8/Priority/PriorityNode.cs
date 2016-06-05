using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.Priority
{
    class PriorityNode:TypesN8Node
    {
        public PriorityNode()
        {
            this.KNXSubNumber = DPST_4;
            this.Name = "priority";
        }

        public static TreeNode GetTypeNode()
        {
            PriorityNode nodeType = new PriorityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
