using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB16.StatusRHCC
{
    class StatusRHCCNode:TypeB16Node
    {
        public StatusRHCCNode()
        {
            this.KNXSubNumber = DPST_101;
            this.DPTName = "RHCC status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusRHCCNode nodeType = new StatusRHCCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
