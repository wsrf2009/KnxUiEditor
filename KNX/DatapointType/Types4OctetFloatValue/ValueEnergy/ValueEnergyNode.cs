using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueEnergy
{
    class ValueEnergyNode:Types4OctetFloatValueNode
    {
        public ValueEnergyNode()
        {
            this.KNXSubNumber = DPST_31;
            this.DPTName = "energy (J)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueEnergyNode nodeType = new ValueEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
