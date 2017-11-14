
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypesV8.PercentV8;
using KNX.DatapointType.TypesV8.StatusMode1;
using KNX.DatapointType.TypesV8.Value1Count;

namespace KNX.DatapointType.TypesV8
{
    class TypesV8Node:DatapointType
    {
        public TypesV8Node()
        {
            this.KNXMainNumber = DPT_6;
            this.DPTName = "8-bit signed value";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesV8Node nodeType = new TypesV8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(PercentV8Node.GetTypeNode());
            nodeType.Nodes.Add(Value1CountNode.GetTypeNode());
            nodeType.Nodes.Add(StatusMode3Node.GetTypeNode());

            return nodeType;
        }
    }
}
