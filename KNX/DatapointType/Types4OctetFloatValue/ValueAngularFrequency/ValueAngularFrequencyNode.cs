using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAngularFrequency
{
    class ValueAngularFrequencyNode:Types4OctetFloatValueNode
    {
        public ValueAngularFrequencyNode()
        {
            this.KNXSubNumber = DPST_34;
            this.DPTName = "angular frequency (rad/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngularFrequencyNode nodeType = new ValueAngularFrequencyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
