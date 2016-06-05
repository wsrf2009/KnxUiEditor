using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueDensity
{
    class ValueDensityNode:Types4OctetFloatValueNode
    {
        public ValueDensityNode()
        {
            this.KNXSubNumber = DPST_17;
            this.Name = "density (kg/m³)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueDensityNode nodeType = new ValueDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
