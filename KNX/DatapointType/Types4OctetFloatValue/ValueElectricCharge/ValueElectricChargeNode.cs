using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricCharge
{
    class ValueElectricChargeNode:Types4OctetFloatValueNode
    {
        public ValueElectricChargeNode()
        {
            this.KNXSubNumber = DPST_18;
            this.DPTName = "electric charge (C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricChargeNode nodeType = new ValueElectricChargeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
