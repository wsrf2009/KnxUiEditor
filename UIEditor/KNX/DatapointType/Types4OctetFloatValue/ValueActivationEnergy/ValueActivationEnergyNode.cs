using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueActivationEnergy
{
    class ValueActivationEnergyNode:Types4OctetFloatValueNode
    {
        public ValueActivationEnergyNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "activation energy (J/mol)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueActivationEnergyNode nodeType = new ValueActivationEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
