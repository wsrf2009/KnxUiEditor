using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeElectricalEnergy.ApparantEnergyV64
{
    class ApparantEnergyV64Node:TypeElectricalEnergyNode
    {
        public ApparantEnergyV64Node()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "apparant energy (VAh)";
        }

        public static TreeNode GetTypeNode()
        {
            ApparantEnergyV64Node nodeType = new ApparantEnergyV64Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
