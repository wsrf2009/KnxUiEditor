using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type3ByteColourRGB.ColourRGB
{
    class ColourRGBNode:Type3ByteColourRGBNode
    {
        public ColourRGBNode()
        {
            this.KNXSubNumber = DPST_600;
            this.DPTName = "RGB value 3x(0..255)";
        }

        public static TreeNode GetTypeNode()
        {
            ColourRGBNode nodeType = new ColourRGBNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
