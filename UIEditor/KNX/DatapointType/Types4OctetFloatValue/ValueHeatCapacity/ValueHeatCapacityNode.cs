using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueHeatCapacity
{
    class ValueHeatCapacityNode:Types4OctetFloatValueNode
    {
        public ValueHeatCapacityNode()
        {
            this.KNXSubNumber = DPST_35;
            this.Name = "heat capacity (J/K)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHeatCapacityNode nodeType = new ValueHeatCapacityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
