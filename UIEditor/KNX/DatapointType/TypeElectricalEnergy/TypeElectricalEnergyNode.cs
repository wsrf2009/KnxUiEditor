using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeB32.CombinedInfoOnOff;
using UIEditor.KNX.DatapointType.TypeElectricalEnergy.ActiveEnergyV64;
using UIEditor.KNX.DatapointType.TypeElectricalEnergy.ApparantEnergyV64;
using UIEditor.KNX.DatapointType.TypeElectricalEnergy.ReactiveEnergyV64;

namespace UIEditor.KNX.DatapointType.TypeElectricalEnergy
{
    class TypeElectricalEnergyNode:DatapointType
    {
        public TypeElectricalEnergyNode()
        {
            this.KNXMainNumber = DPT_29;
            this.Name = "electrical energy";
            this.Type = KNXDataType.Bit64;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeElectricalEnergyNode nodeType = new TypeElectricalEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ActiveEnergyV64Node.GetTypeNode());
            nodeType.Nodes.Add(ApparantEnergyV64Node.GetTypeNode());
            nodeType.Nodes.Add(ReactiveEnergyV64Node.GetTypeNode());

            return nodeType;
        }
    }
}
