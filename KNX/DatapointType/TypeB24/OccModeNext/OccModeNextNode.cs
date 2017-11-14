using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB24.OccModeNext
{
    class OccModeNextNode:TypeB24Node
    {
        public OccModeNextNode()
        {
            this.KNXSubNumber = DPST_104;
            this.DPTName = "time delay & occupancy mode";
        }

        public static TreeNode GetTypeNode()
        {
            OccModeNextNode nodeType = new OccModeNextNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
