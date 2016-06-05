using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueEnergy
{
    class ValueEnergyNode:Types4OctetFloatValueNode
    {
        public ValueEnergyNode()
        {
            this.KNXSubNumber = DPST_31;
            this.Name = "energy (J)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueEnergyNode nodeType = new ValueEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
