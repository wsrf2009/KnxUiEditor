using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValuePowerFactor
{
    class ValuePowerFactorNode:Types4OctetFloatValueNode
    {
        public ValuePowerFactorNode()
        {
            this.KNXSubNumber = DPST_57;
            this.DPTName = "power factor (cos Φ)";
        }

        public static TreeNode GetTypeNode()
        {
            ValuePowerFactorNode nodeType = new ValuePowerFactorNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
