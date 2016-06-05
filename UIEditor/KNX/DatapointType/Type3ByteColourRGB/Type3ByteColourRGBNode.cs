using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Type3ByteColourRGB.ColourRGB;

namespace UIEditor.KNX.DatapointType.Type3ByteColourRGB
{
    class Type3ByteColourRGBNode:DatapointType
    {
        public Type3ByteColourRGBNode()
        {
            this.KNXMainNumber = DPT_232;
            this.Name = "3-byte colour RGB";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type3ByteColourRGBNode nodeType = new Type3ByteColourRGBNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ColourRGBNode.GetTypeNode());

            return nodeType;
        }
    }
}
