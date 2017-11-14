using KNX.DatapointType.Type3ByteColourRGB.ColourRGB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type3ByteColourRGB
{
    class Type3ByteColourRGBNode:DatapointType
    {
        public Type3ByteColourRGBNode()
        {
            this.KNXMainNumber = DPT_232;
            this.DPTName = "3-byte colour RGB";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type3ByteColourRGBNode nodeType = new Type3ByteColourRGBNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(ColourRGBNode.GetTypeNode());

            return nodeType;
        }
    }
}
