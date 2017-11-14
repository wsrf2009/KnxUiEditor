
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB8.ChannelActivation8;
using KNX.DatapointType.TypeB8.DeviceControl;
using KNX.DatapointType.TypeB8.ForceSign;
using KNX.DatapointType.TypeB8.ForceSignCool;
using KNX.DatapointType.TypeB8.FuelTypeSet;
using KNX.DatapointType.TypeB8.LightActuatorErrorInfo;
using KNX.DatapointType.TypeB8.RFFilterInfo;
using KNX.DatapointType.TypeB8.RFModeInfo;
using KNX.DatapointType.TypeB8.StatusAHU;
using KNX.DatapointType.TypeB8.StatusGen;
using KNX.DatapointType.TypeB8.StatusRCC;
using KNX.DatapointType.TypeB8.StatusRHC;
using KNX.DatapointType.TypeB8.StatusSDHWC;

namespace KNX.DatapointType.TypeB8
{
    class TypeB8Node:DatapointType
    {
        public TypeB8Node()
        {
            this.KNXMainNumber = DPT_21;
            this.DPTName = "8-bit set";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB8Node nodeType = new TypeB8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(StatusGenNode.GetTypeNode());
            nodeType.Nodes.Add(DeviceControlNode.GetTypeNode());
            nodeType.Nodes.Add(ForceSignNode.GetTypeNode());
            nodeType.Nodes.Add(ForceSignCoolNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRHCNode.GetTypeNode());
            nodeType.Nodes.Add(StatusSDHWCNode.GetTypeNode());
            nodeType.Nodes.Add(FuelTypeSetNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRCCNode.GetTypeNode());
            nodeType.Nodes.Add(StatusAHUNode.GetTypeNode());
            nodeType.Nodes.Add(LightActuatorErrorInfoNode.GetTypeNode());
            nodeType.Nodes.Add(RFModeInfoNode.GetTypeNode());
            nodeType.Nodes.Add(RFFilterInfoNode.GetTypeNode());
            nodeType.Nodes.Add(ChannelActivation8Node.GetTypeNode());

            return nodeType;
        }
    }
}
