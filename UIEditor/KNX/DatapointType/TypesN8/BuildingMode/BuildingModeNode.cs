using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.BuildingMode
{
    class BuildingModeNode:TypesN8Node
    {
        public BuildingModeNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "building mode";
        }

        public static TreeNode GetTypeNode()
        {
            BuildingModeNode nodeType = new BuildingModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
