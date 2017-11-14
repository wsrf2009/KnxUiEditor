using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB8.StatusSDHWC
{
    class StatusSDHWCNode:TypeB8Node
    {
        public StatusSDHWCNode()
        {
            this.KNXSubNumber = DPST_103;
            this.DPTName = "solar DHW controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusSDHWCNode nodeType = new StatusSDHWCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
