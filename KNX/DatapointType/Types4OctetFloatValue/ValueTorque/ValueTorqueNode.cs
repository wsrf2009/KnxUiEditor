using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueTorque
{
    class ValueTorqueNode:Types4OctetFloatValueNode
    {
        public ValueTorqueNode()
        {
            this.KNXSubNumber = DPST_75;
            this.DPTName = "torque (Nm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTorqueNode nodeType = new ValueTorqueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
