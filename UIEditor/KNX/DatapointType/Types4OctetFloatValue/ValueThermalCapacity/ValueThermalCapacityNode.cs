using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueThermalCapacity
{
    class ValueThermalCapacityNode:Types4OctetFloatValueNode
    {
        public ValueThermalCapacityNode()
        {
            this.KNXSubNumber = DPST_71;
            this.Name = "thermal capacity (J/K)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueThermalCapacityNode nodeType = new ValueThermalCapacityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
