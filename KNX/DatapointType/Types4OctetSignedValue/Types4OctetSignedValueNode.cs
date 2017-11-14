using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.Types4OctetSignedValue.ActiveEnergy;
using KNX.DatapointType.Types4OctetSignedValue.ActiveEnergykWh;
using KNX.DatapointType.Types4OctetSignedValue.ApparantEnergy;
using KNX.DatapointType.Types4OctetSignedValue.ApparantEnergykVAh;
using KNX.DatapointType.Types4OctetSignedValue.FlowRatem3h;
using KNX.DatapointType.Types4OctetSignedValue.LongDeltaTimeSec;
using KNX.DatapointType.Types4OctetSignedValue.ReactiveEnergy;
using KNX.DatapointType.Types4OctetSignedValue.ReactiveEnergykVARh;
using KNX.DatapointType.Types4OctetSignedValue.Value4Count;

namespace KNX.DatapointType.Types4OctetSignedValue
{
    class Types4OctetSignedValueNode:DatapointType
    {
        public Types4OctetSignedValueNode()
        {
            this.KNXMainNumber = DPT_13;
            this.DPTName = "4-byte signed value";
        }

        public static TreeNode GetAllTypeNode()
        {
            Types4OctetSignedValueNode nodeType = new Types4OctetSignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(Value4CountNode.GetTypeNode());
            nodeType.Nodes.Add(FlowRatem3hNode.GetTypeNode());
            nodeType.Nodes.Add(ActiveEnergyNode.GetTypeNode());
            nodeType.Nodes.Add(ApparantEnergyNode.GetTypeNode());
            nodeType.Nodes.Add(ReactiveEnergyNode.GetTypeNode());
            nodeType.Nodes.Add(ActiveEnergykWhNode.GetTypeNode());
            nodeType.Nodes.Add(ApparantEnergykVAhNode.GetTypeNode());
            nodeType.Nodes.Add(ReactiveEnergykVARhNode.GetTypeNode());
            nodeType.Nodes.Add(LongDeltaTimeSecNode.GetTypeNode());

            return nodeType;
        }
    }
}
