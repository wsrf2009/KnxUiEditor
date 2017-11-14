using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAcceleration
{
    class ValueAccelerationNode:Types4OctetFloatValueNode
    {
        public ValueAccelerationNode()
        {
            this.KNXSubNumber = DPST_0;
            this.DPTName = "acceleration (m/s²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAccelerationNode nodeType = new ValueAccelerationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
