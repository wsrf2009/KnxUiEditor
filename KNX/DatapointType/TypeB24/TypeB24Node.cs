using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB24.BuildingModeNext;
using KNX.DatapointType.TypeB24.DHWModeNext;
using KNX.DatapointType.TypeB24.HVACModeNext;
using KNX.DatapointType.TypeB24.OccModeNext;

namespace KNX.DatapointType.TypeB24
{
    class TypeB24Node:DatapointType
    {
        public TypeB24Node()
        {
            this.KNXMainNumber = DPT_206;
            this.DPTName = "16-bit unsigned value & 8-bit enum";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB24Node nodeType = new TypeB24Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(HVACModeNextNode.GetTypeNode());
            nodeType.Nodes.Add(DHWModeNextNode.GetTypeNode());
            nodeType.Nodes.Add(OccModeNextNode.GetTypeNode());
            nodeType.Nodes.Add(BuildingModeNextNode.GetTypeNode());

            return nodeType;
        }
    }
}
