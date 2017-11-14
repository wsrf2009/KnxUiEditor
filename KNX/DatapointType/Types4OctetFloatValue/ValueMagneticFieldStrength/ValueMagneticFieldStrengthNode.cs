using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFieldStrength
{
    class ValueMagneticFieldStrengthNode:Types4OctetFloatValueNode
    {
        public ValueMagneticFieldStrengthNode()
        {
            this.KNXSubNumber = DPST_44;
            this.DPTName = "magnetic field strength (A/m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticFieldStrengthNode nodeType = new ValueMagneticFieldStrengthNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
