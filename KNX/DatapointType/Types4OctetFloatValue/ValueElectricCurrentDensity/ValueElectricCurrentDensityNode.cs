using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricCurrentDensity
{
    class ValueElectricCurrentDensityNode:Types4OctetFloatValueNode
    {
        public ValueElectricCurrentDensityNode()
        {
            this.KNXSubNumber = DPST_20;
            this.DPTName = "electric current density (A/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricCurrentDensityNode nodeType = new ValueElectricCurrentDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
