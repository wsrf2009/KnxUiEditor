using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricDipoleMoment
{
    class ValueElectricDipoleMomentNode:Types4OctetFloatValueNode
    {
        public ValueElectricDipoleMomentNode()
        {
            this.KNXSubNumber = DPST_21;
            this.Name = "electric dipole moment (Cm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricDipoleMomentNode nodeType = new ValueElectricDipoleMomentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
