using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.ForceSign
{
    class ForceSignNode:TypeB8Node
    {
        public ForceSignNode()
        {
            this.KNXSubNumber = DPST_100;
            this.Name = "forcing signal";
        }

        public static TreeNode GetTypeNode()
        {
            ForceSignNode nodeType = new ForceSignNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
