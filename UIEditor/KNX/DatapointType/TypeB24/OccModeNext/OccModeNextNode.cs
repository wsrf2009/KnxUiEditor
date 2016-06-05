using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB24.OccModeNext
{
    class OccModeNextNode:TypeB24Node
    {
        public OccModeNextNode()
        {
            this.KNXSubNumber = DPST_104;
            this.Name = "time delay & occupancy mode";
        }

        public static TreeNode GetTypeNode()
        {
            OccModeNextNode nodeType = new OccModeNextNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
