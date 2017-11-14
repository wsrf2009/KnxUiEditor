using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricPotential
{
    class ValueElectricPotentialNode:Types4OctetFloatValueNode
    {
        public ValueElectricPotentialNode()
        {
            this.KNXSubNumber = DPST_27;
            this.DPTName = "electric potential (V)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricPotentialNode nodeType = new ValueElectricPotentialNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
