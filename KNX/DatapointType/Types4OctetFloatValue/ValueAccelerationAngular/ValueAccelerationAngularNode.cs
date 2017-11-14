using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAccelerationAngular
{
    class ValueAccelerationAngularNode:Types4OctetFloatValueNode
    {
        public ValueAccelerationAngularNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "angular acceleration (rad/s²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAccelerationAngularNode nodeType = new ValueAccelerationAngularNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
