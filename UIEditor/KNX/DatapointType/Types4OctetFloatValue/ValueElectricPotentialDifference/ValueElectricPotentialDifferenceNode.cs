using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricPotentialDifference
{
    class ValueElectricPotentialDifferenceNode:Types4OctetFloatValueNode
    {
        public ValueElectricPotentialDifferenceNode()
        {
            this.KNXSubNumber = DPST_28;
            this.Name = "electric potential difference (V)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricPotentialDifferenceNode nodeType = new ValueElectricPotentialDifferenceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
