using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB8.StatusRHC
{
    class StatusRHCNode:TypeB8Node
    {
        public StatusRHCNode()
        {
            this.KNXSubNumber = DPST_102;
            this.DPTName = "room heating controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusRHCNode nodeType = new StatusRHCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
