using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePower
{
    class ValuePowerNode:Types4OctetFloatValueNode
    {
        public ValuePowerNode()
        {
            this.KNXSubNumber = DPST_56;
            this.Name = "power (W)";
        }

        public static TreeNode GetTypeNode()
        {
            ValuePowerNode nodeType = new ValuePowerNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
