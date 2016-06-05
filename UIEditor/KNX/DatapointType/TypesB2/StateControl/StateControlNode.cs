using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.StateControl
{
    class StateControlNode:TypesB2Node
    {
        public StateControlNode()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "state control";
        }

        public static TreeNode GetTypeNode()
        {
            StateControlNode nodeType = new StateControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
