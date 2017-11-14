using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueLightQuantity
{
    class ValueLightQuantityNode:Types4OctetFloatValueNode
    {
        public ValueLightQuantityNode()
        {
            this.KNXSubNumber = DPST_40;
            this.DPTName = "light quantity (J)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLightQuantityNode nodeType = new ValueLightQuantityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
