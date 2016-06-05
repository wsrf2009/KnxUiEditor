using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ValveMode
{
    class ValveModeNode:TypesN8Node
    {
        public ValveModeNode()
        {
            this.KNXSubNumber = DPST_108;
            this.Name = "valve mode";
        }

        public static TreeNode GetTypeNode()
        {
            ValveModeNode nodeType = new ValveModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
