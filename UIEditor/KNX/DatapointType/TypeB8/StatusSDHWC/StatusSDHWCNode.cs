using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.StatusSDHWC
{
    class StatusSDHWCNode:TypeB8Node
    {
        public StatusSDHWCNode()
        {
            this.KNXSubNumber = DPST_103;
            this.Name = "solar DHW controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusSDHWCNode nodeType = new StatusSDHWCNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
