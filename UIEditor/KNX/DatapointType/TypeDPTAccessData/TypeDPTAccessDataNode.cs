using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDPTAccessData.AccessData;

namespace UIEditor.KNX.DatapointType.TypeDPTAccessData
{
    class TypeDPTAccessDataNode:DatapointType
    {
        public TypeDPTAccessDataNode()
        {
            this.KNXMainNumber = DPT_15;
            this.Name = "entrance access";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTAccessDataNode nodeType = new TypeDPTAccessDataNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(AccessDataNode.GetTypeNode());
            

            return nodeType;
        }
    }
}
