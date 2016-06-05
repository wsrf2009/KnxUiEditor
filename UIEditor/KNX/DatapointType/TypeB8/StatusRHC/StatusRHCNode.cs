using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.StatusRHC
{
    class StatusRHCNode:TypeB8Node
    {
        public StatusRHCNode()
        {
            this.KNXSubNumber = DPST_102;
            this.Name = "room heating controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusRHCNode nodeType = new StatusRHCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
