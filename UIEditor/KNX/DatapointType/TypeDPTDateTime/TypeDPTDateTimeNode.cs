using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDPTDateTime.DateTime;

namespace UIEditor.KNX.DatapointType.TypeDPTDateTime
{
    class TypeDPTDateTimeNode:DatapointType
    {
        public TypeDPTDateTimeNode()
        {
            this.KNXMainNumber = DPT_19;
            this.Name = "Date Time";
            this.Type = KNXDataType.Bit64;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTDateTimeNode nodeType = new TypeDPTDateTimeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(DateTimeNode.GetTypeNode());

            return nodeType;
        }
    }
}
