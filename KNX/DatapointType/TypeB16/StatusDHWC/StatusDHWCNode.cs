using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB16.StatusDHWC
{
    class StatusDHWCNode:TypeB16Node
    {
        public StatusDHWCNode()
        {
            this.KNXSubNumber = DPST_100;
            this.DPTName = "DHW controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusDHWCNode nodeType = new StatusDHWCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
