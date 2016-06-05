using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricFlux
{
    class ValueElectricFluxNode:Types4OctetFloatValueNode
    {
        public ValueElectricFluxNode()
        {
            this.KNXSubNumber = DPST_24;
            this.Name = "electric flux (C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricFluxNode nodeType = new ValueElectricFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
