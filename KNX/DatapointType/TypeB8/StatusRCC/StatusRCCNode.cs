using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB8.StatusRCC
{
    class StatusRCCNode:TypeB8Node
    {
        public StatusRCCNode()
        {
            this.KNXSubNumber = DPST_105;
            this.DPTName = "room cooling controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusRCCNode nodeType = new StatusRCCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
