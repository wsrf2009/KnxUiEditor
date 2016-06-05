using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueHeatQuantity
{
    class ValueHeatQuantityNode:Types4OctetFloatValueNode
    {
        public ValueHeatQuantityNode()
        {
            this.KNXSubNumber = DPST_37;
            this.Name = "heat quantity";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHeatQuantityNode nodeType = new ValueHeatQuantityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
