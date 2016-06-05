using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMassFlux
{
    class ValueMassFluxNode:Types4OctetFloatValueNode
    {
        public ValueMassFluxNode()
        {
            this.KNXSubNumber = DPST_52;
            this.Name = "mass flux (kg/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMassFluxNode nodeType = new ValueMassFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
