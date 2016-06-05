using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.BoolControl
{
    class BoolControlNode : TypesB2Node
    {
        public BoolControlNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "boolean control";
        }

        public static TreeNode GetTypeNode()
        {
            BoolControlNode nodeType = new BoolControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
