using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFluxDensity
{
    class ValueMagneticFluxDensityNode:Types4OctetFloatValueNode
    {
        public ValueMagneticFluxDensityNode()
        {
            this.KNXSubNumber = DPST_46;
            this.Name = "magnetic flux density (T)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticFluxDensityNode nodeType = new ValueMagneticFluxDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
