using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFluxDensity
{
    class ValueMagneticFluxDensityNode:Types4OctetFloatValueNode
    {
        public ValueMagneticFluxDensityNode()
        {
            this.KNXSubNumber = DPST_46;
            this.DPTName = "magnetic flux density (T)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticFluxDensityNode nodeType = new ValueMagneticFluxDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
