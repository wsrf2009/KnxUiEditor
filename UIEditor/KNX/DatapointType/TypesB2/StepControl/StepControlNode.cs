using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.StepControl
{
    class StepControlNode:TypesB2Node
    {
        public StepControlNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "step control";
        }

        public static TreeNode GetTypeNode()
        {
            StepControlNode nodeType = new StepControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
