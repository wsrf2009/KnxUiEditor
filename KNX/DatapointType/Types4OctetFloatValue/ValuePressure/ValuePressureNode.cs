using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValuePressure
{
    class ValuePressureNode:Types4OctetFloatValueNode
    {
        public ValuePressureNode()
        {
            this.KNXSubNumber = DPST_58;
            this.DPTName = "pressure (Pa)";
        }

        public static TreeNode GetTypeNode()
        {
            ValuePressureNode nodeType = new ValuePressureNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
