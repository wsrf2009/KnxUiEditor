using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types8BitUnsignedValue.PercentU8
{
    class PercentU8Node:Types8BitUnsignedValueNode
    {
        public PercentU8Node()
        {
            this.KNXSubNumber = DPST_4;
            this.DPTName = "percentage (0..255%)";
        }

        public static TreeNode GetTypeNode()
        {
            PercentU8Node nodeType = new PercentU8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
