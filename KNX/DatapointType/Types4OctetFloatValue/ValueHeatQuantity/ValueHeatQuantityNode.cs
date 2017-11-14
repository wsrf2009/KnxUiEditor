using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueHeatQuantity
{
    class ValueHeatQuantityNode:Types4OctetFloatValueNode
    {
        public ValueHeatQuantityNode()
        {
            this.KNXSubNumber = DPST_37;
            this.DPTName = "heat quantity";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHeatQuantityNode nodeType = new ValueHeatQuantityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
