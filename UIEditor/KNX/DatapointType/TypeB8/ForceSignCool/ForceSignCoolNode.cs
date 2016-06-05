using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.ForceSignCool
{
    class ForceSignCoolNode:TypeB8Node
    {
        public ForceSignCoolNode()
        {
            this.KNXSubNumber = DPST_101;
            this.Name = "forcing signal cool";
        }

        public static TreeNode GetTypeNode()
        {
            ForceSignCoolNode nodeType = new ForceSignCoolNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
