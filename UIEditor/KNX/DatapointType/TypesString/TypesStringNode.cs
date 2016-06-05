using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesString.String88591;
using UIEditor.KNX.DatapointType.TypesString.StringASCII;

namespace UIEditor.KNX.DatapointType.TypesString
{
    class TypesStringNode:DatapointType
    {
        public TypesStringNode()
        {
            this.KNXMainNumber = DPT_16;
            this.Name = "character string";
            this.Type = KNXDataType.Bit112;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesStringNode nodeType = new TypesStringNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(StringASCIINode.GetTypeNode());
            nodeType.Nodes.Add(String88591Node.GetTypeNode());

            return nodeType;
        }
    }
}
