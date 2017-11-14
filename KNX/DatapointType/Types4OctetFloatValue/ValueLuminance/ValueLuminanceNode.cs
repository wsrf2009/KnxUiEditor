using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueLuminance
{
    class ValueLuminanceNode:Types4OctetFloatValueNode
    {
        public ValueLuminanceNode()
        {
            this.KNXSubNumber = DPST_41;
            this.DPTName = "luminance (cd/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLuminanceNode nodeType = new ValueLuminanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
