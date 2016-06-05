using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.BlinkingMode
{
    class BlinkingModeNode:TypesN8Node
    {
        public BlinkingModeNode()
        {
            this.KNXSubNumber = DPST_603;
            this.Name = "blink mode";
        }

        public static TreeNode GetTypeNode()
        {
            BlinkingModeNode nodeType = new BlinkingModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
