using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueFrequency
{
    class ValueFrequencyNode:Types4OctetFloatValueNode
    {
        public ValueFrequencyNode()
        {
            this.KNXSubNumber = DPST_33;
            this.DPTName = "frequency (Hz)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueFrequencyNode nodeType = new ValueFrequencyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
