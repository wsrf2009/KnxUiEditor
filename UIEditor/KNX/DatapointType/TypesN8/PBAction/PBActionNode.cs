using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.PBAction
{
    class PBActionNode:TypesN8Node
    {
        public PBActionNode()
        {
            this.KNXSubNumber = DPST_606;
            this.Name = "PB action mode";
        }

        public static TreeNode GetTypeNode()
        {
            PBActionNode nodeType = new PBActionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
