using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePhaseAngleDeg
{
    class ValuePhaseAngleDegNode:Types4OctetFloatValueNode
    {
        public ValuePhaseAngleDegNode()
        {
            this.KNXSubNumber = DPST_55;
            this.Name = "phase angle (°)";
        }

        public static TreeNode GetTypeNode()
        {
            ValuePhaseAngleDegNode nodeType = new ValuePhaseAngleDegNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
