using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.DHWMode
{
    class DHWModeNode:TypesN8Node
    {
        public DHWModeNode()
        {
            this.KNXSubNumber = DPST_103;
            this.Name = "DHW mode";
        }

        public static TreeNode GetTypeNode()
        {
            DHWModeNode nodeType = new DHWModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
