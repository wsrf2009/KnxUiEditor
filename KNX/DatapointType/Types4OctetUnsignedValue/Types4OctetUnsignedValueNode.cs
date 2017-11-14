using KNX.DatapointType.Types4OctetUnsignedValue.Value4Ucount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetUnsignedValue
{
    class Types4OctetUnsignedValueNode:DatapointType
    {
        public Types4OctetUnsignedValueNode()
        {
            this.KNXMainNumber = DPT_12;
            this.DPTName = "4-byte unsigned value";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types4OctetUnsignedValueNode nodeType = new Types4OctetUnsignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(Value4UcountNode.GetTypeNode());

            return nodeType;
        }
    }
}
