
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB32.CombinedInfoOnOff;

namespace KNX.DatapointType.TypeB32
{
    class TypeB32Node:DatapointType
    {
        public TypeB32Node()
        {
            this.KNXMainNumber = DPT_27;
            this.DPTName = "32-bit set";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB32Node nodeType = new TypeB32Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(CombinedInfoOnOffNode.GetTypeNode());

            return nodeType;
        }
    }
}
