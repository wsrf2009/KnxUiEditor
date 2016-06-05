using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDate.Date;

namespace UIEditor.KNX.DatapointType.TypeDate
{
    class TypeDateNode:DatapointType
    {
        public TypeDateNode()
        {
            this.KNXMainNumber = DPT_11;
            this.Name = "date";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDateNode nodeType = new TypeDateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(DateNode.GetTypeNode());

            return nodeType;
        }
    }
}
