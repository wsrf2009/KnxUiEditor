using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.RainAmount
{
    class RainAmountNode:Types2OctetFloatValueNode
    {
        public RainAmountNode()
        {
            this.KNXSubNumber = DPST_26;
            this.DPTName = "rain amount (l/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            RainAmountNode nodeType = new RainAmountNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
