using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB24.BuildingModeNext
{
    class BuildingModeNextNode:TypeB24Node
    {
        public BuildingModeNextNode()
        {
            this.KNXSubNumber = DPST_105;
            this.DPTName = "time delay & building mode";
        }

        public static TreeNode GetTypeNode()
        {
            BuildingModeNextNode nodeType = new BuildingModeNextNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
