using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.StateControl
{
    class StateControlNode:TypesB2Node
    {
        public StateControlNode()
        {
            this.KNXSubNumber = DPST_11;
            this.DPTName = "state control";
        }

        public static TreeNode GetTypeNode()
        {
            StateControlNode nodeType = new StateControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
