using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueThermalConductivity
{
    class ValueThermalConductivityNode:Types4OctetFloatValueNode
    {
        public ValueThermalConductivityNode()
        {
            this.KNXSubNumber = DPST_72;
            this.Name = "thermal conductivity (W/mK)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueThermalConductivityNode nodeType = new ValueThermalConductivityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
