using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularVelocity
{
    class ValueAngularVelocityNode:Types4OctetFloatValueNode
    {
        public ValueAngularVelocityNode()
        {
            this.KNXSubNumber = DPST_9;
            this.Name = "angular velocity (rad/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngularVelocityNode nodeType = new ValueAngularVelocityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
