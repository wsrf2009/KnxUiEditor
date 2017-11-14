using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAngleDeg
{
    class ValueAngleDegNode:Types4OctetFloatValueNode
    {
        public ValueAngleDegNode()
        {
            this.KNXSubNumber = DPST_7;
            this.DPTName = "angle (degree)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngleDegNode nodeType = new ValueAngleDegNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
