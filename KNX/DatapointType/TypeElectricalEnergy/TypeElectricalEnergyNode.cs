using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB32.CombinedInfoOnOff;
using KNX.DatapointType.TypeElectricalEnergy.ActiveEnergyV64;
using KNX.DatapointType.TypeElectricalEnergy.ApparantEnergyV64;
using KNX.DatapointType.TypeElectricalEnergy.ReactiveEnergyV64;

namespace KNX.DatapointType.TypeElectricalEnergy
{
    class TypeElectricalEnergyNode:DatapointType
    {
        public TypeElectricalEnergyNode()
        {
            this.KNXMainNumber = DPT_29;
            this.DPTName = "electrical energy";
            this.Type = KNXDataType.Bit64;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeElectricalEnergyNode nodeType = new TypeElectricalEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(ActiveEnergyV64Node.GetTypeNode());
            nodeType.Nodes.Add(ApparantEnergyV64Node.GetTypeNode());
            nodeType.Nodes.Add(ReactiveEnergyV64Node.GetTypeNode());

            return nodeType;
        }
    }
}
