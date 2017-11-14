using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueLuminousIntensity
{
    class ValueLuminousIntensityNode:Types4OctetFloatValueNode
    {
        public ValueLuminousIntensityNode()
        {
            this.KNXSubNumber = DPST_43;
            this.DPTName = "luminous intensity (cd)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLuminousIntensityNode nodeType = new ValueLuminousIntensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
