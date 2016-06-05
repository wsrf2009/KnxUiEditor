using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types4OctetUnsignedValue.Value4Ucount;

namespace UIEditor.KNX.DatapointType.Types4OctetUnsignedValue
{
    class Types4OctetUnsignedValueNode:DatapointType
    {
        public Types4OctetUnsignedValueNode()
        {
            this.KNXMainNumber = DPT_12;
            this.Name = "4-byte unsigned value";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types4OctetUnsignedValueNode nodeType = new Types4OctetUnsignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(Value4UcountNode.GetTypeNode());

            return nodeType;
        }
    }
}
