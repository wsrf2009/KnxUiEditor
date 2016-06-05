using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.Value2Ucount
{
    class Value2UcountNode:Types2OctetUnsignedValueNode
    {
        public Value2UcountNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "pulses";
        }

        public static TreeNode GetTypeNode()
        {
            Value2UcountNode nodeType = new Value2UcountNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
