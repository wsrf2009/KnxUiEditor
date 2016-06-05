using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB16.StatusDHWC
{
    class StatusDHWCNode:TypeB16Node
    {
        public StatusDHWCNode()
        {
            this.KNXSubNumber = DPST_100;
            this.Name = "DHW controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusDHWCNode nodeType = new StatusDHWCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
