using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularFrequency
{
    class ValueAngularFrequencyNode:Types4OctetFloatValueNode
    {
        public ValueAngularFrequencyNode()
        {
            this.KNXSubNumber = DPST_34;
            this.Name = "angular frequency (rad/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngularFrequencyNode nodeType = new ValueAngularFrequencyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
