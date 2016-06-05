using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesCharacterSet.CharASCII;
using UIEditor.KNX.DatapointType.TypesCharacterSet.Char88591;

namespace UIEditor.KNX.DatapointType.TypesCharacterSet
{
    class TypesCharacterSetNode:DatapointType
    {
        public TypesCharacterSetNode()
        {
            this.KNXMainNumber = DPT_4;
            this.Name = "character";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesCharacterSetNode nodeType = new TypesCharacterSetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(CharASCIINode.GetTypeNode());
            nodeType.Nodes.Add(Char88591Node.GetTypeNode());

            return nodeType;
        }
    }
}
