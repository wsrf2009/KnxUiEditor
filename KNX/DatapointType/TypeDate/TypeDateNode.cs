using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDate.Date;

namespace KNX.DatapointType.TypeDate
{
    class TypeDateNode:DatapointType
    {
        public TypeDateNode()
        {
            this.KNXMainNumber = DPT_11;
            this.DPTName = "date";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDateNode nodeType = new TypeDateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(DateNode.GetTypeNode());

            return nodeType;
        }
    }
}
