using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types8BitUnsignedValue.Value1Ucount
{
    class Value1UcountNode:Types8BitUnsignedValueNode
    {
        public Value1UcountNode()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "counter pulses (0..255)";
        }

        public static TreeNode GetTypeNode()
        {
            Value1UcountNode nodeType = new Value1UcountNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
