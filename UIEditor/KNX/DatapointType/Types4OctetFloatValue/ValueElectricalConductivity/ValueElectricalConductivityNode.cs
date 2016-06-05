using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricalConductivity
{
    class ValueElectricalConductivityNode:Types4OctetFloatValueNode
    {
        public ValueElectricalConductivityNode()
        {
            this.KNXSubNumber = DPST_16;
            this.Name = "conductivity  (S/m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricalConductivityNode nodeType = new ValueElectricalConductivityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
