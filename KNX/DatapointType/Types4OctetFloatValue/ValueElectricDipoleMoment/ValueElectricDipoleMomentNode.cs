using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricDipoleMoment
{
    class ValueElectricDipoleMomentNode:Types4OctetFloatValueNode
    {
        public ValueElectricDipoleMomentNode()
        {
            this.KNXSubNumber = DPST_21;
            this.DPTName = "electric dipole moment (Cm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricDipoleMomentNode nodeType = new ValueElectricDipoleMomentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
