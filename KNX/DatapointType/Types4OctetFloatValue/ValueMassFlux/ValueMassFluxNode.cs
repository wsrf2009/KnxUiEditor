using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMassFlux
{
    class ValueMassFluxNode:Types4OctetFloatValueNode
    {
        public ValueMassFluxNode()
        {
            this.KNXSubNumber = DPST_52;
            this.DPTName = "mass flux (kg/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMassFluxNode nodeType = new ValueMassFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
