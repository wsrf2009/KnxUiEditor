using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB8.StatusAHU
{
    class StatusAHUNode:TypeB8Node
    {
        public StatusAHUNode()
        {
            this.KNXSubNumber = DPST_106;
            this.DPTName = "ventilation controller status";
        }

        public static TreeNode GetTypeNode()
        {
            StatusAHUNode nodeType = new StatusAHUNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
