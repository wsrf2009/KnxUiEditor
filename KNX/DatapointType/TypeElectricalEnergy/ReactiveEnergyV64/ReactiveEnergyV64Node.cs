using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeElectricalEnergy.ReactiveEnergyV64
{
    class ReactiveEnergyV64Node:TypeElectricalEnergyNode
    {
        public ReactiveEnergyV64Node()
        {
            this.KNXSubNumber = DPST_12;
            this.DPTName = "reactive energy (VARh)";
        }

        public static TreeNode GetTypeNode()
        {
            ReactiveEnergyV64Node nodeType = new ReactiveEnergyV64Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
