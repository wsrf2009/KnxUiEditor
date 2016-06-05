using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueFrequency
{
    class ValueFrequencyNode:Types4OctetFloatValueNode
    {
        public ValueFrequencyNode()
        {
            this.KNXSubNumber = DPST_33;
            this.Name = "frequency (Hz)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueFrequencyNode nodeType = new ValueFrequencyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
