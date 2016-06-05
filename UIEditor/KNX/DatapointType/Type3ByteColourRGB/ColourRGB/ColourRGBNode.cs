using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Type3ByteColourRGB.ColourRGB
{
    class ColourRGBNode:Type3ByteColourRGBNode
    {
        public ColourRGBNode()
        {
            this.KNXSubNumber = DPST_600;
            this.Name = "RGB value 3x(0..255)";
        }

        public static TreeNode GetTypeNode()
        {
            ColourRGBNode nodeType = new ColourRGBNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
