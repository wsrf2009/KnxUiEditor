using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricFlux
{
    class ValueElectricFluxNode:Types4OctetFloatValueNode
    {
        public ValueElectricFluxNode()
        {
            this.KNXSubNumber = DPST_24;
            this.DPTName = "electric flux (C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricFluxNode nodeType = new ValueElectricFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
