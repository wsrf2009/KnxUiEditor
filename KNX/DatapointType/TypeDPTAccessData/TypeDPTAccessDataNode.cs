using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDPTAccessData.AccessData;

namespace KNX.DatapointType.TypeDPTAccessData
{
    class TypeDPTAccessDataNode:DatapointType
    {
        public TypeDPTAccessDataNode()
        {
            this.KNXMainNumber = DPT_15;
            this.DPTName = "entrance access";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTAccessDataNode nodeType = new TypeDPTAccessDataNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(AccessDataNode.GetTypeNode());
            

            return nodeType;
        }
    }
}
