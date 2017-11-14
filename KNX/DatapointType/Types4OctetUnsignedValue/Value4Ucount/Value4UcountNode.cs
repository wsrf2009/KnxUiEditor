using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetUnsignedValue.Value4Ucount
{
    class Value4UcountNode:Types4OctetUnsignedValueNode
    {
        public Value4UcountNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "counter pulses (unsigned)";
        }

        public static TreeNode GetTypeNode()
        {
            Value4UcountNode nodeType = new Value4UcountNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
