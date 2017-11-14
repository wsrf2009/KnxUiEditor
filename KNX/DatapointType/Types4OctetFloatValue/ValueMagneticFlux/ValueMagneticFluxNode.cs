using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFlux
{
    class ValueMagneticFluxNode:Types4OctetFloatValueNode
    {
        public ValueMagneticFluxNode()
        {
            this.KNXSubNumber = DPST_45;
            this.DPTName = "magnetic flux (Wb)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticFluxNode nodeType = new ValueMagneticFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
