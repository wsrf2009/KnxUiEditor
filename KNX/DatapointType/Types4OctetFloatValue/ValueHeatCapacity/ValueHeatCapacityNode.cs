using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueHeatCapacity
{
    class ValueHeatCapacityNode:Types4OctetFloatValueNode
    {
        public ValueHeatCapacityNode()
        {
            this.KNXSubNumber = DPST_35;
            this.DPTName = "heat capacity (J/K)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHeatCapacityNode nodeType = new ValueHeatCapacityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
