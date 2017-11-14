using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.BuildingMode
{
    class BuildingModeNode:TypesN8Node
    {
        public BuildingModeNode()
        {
            this.KNXSubNumber = DPST_2;
            this.DPTName = "building mode";
        }

        public static TreeNode GetTypeNode()
        {
            BuildingModeNode nodeType = new BuildingModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
