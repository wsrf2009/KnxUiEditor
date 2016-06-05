using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.RampControl
{
    class RampControlNode:TypesB2Node
    {
        public RampControlNode()
        {
            this.KNXSubNumber = DPST_4;
            this.Name = "ramp control";
        }

        public static TreeNode GetTypeNode()
        {
            RampControlNode nodeType = new RampControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
