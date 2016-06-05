using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLuminousIntensity
{
    class ValueLuminousIntensityNode:Types4OctetFloatValueNode
    {
        public ValueLuminousIntensityNode()
        {
            this.KNXSubNumber = DPST_43;
            this.Name = "luminous intensity (cd)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLuminousIntensityNode nodeType = new ValueLuminousIntensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
