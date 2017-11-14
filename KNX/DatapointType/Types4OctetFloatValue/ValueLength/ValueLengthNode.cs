using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueLength
{
    class ValueLengthNode:Types4OctetFloatValueNode
    {
        public ValueLengthNode()
        {
            this.KNXSubNumber = DPST_39;
            this.DPTName = "length (m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLengthNode nodeType = new ValueLengthNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
