using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB24.DHWModeNext
{
    class DHWModeNextNode:TypeB24Node
    {
        public DHWModeNextNode()
        {
            this.KNXSubNumber = DPST_102;
            this.DPTName = "time delay & DHW mode";
        }

        public static TreeNode GetTypeNode()
        {
            DHWModeNextNode nodeType = new DHWModeNextNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
