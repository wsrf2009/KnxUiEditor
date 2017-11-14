using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueThermalConductivity
{
    class ValueThermalConductivityNode:Types4OctetFloatValueNode
    {
        public ValueThermalConductivityNode()
        {
            this.KNXSubNumber = DPST_72;
            this.DPTName = "thermal conductivity (W/mK)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueThermalConductivityNode nodeType = new ValueThermalConductivityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
