using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types8BitUnsignedValue.DecimalFactor
{
    class DecimalFactorNode:Types8BitUnsignedValueNode
    {
        public DecimalFactorNode()
        {
            this.KNXSubNumber = DPST_5;
            this.DPTName = "ratio (0..255)";
        }

        public static TreeNode GetTypeNode()
        {
            DecimalFactorNode nodeType = new DecimalFactorNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
