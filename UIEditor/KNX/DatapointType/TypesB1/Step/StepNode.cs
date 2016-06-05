using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Step
{
    class StepNode : TypesB1Node
    {
        public StepNode()
        {
            //this.Text = "1.007 step";
            this.KNXSubNumber = DPST_7;
            this.Name = "step";
        }

        public static TreeNode GetTypeNode()
        {
            StepNode nodeType = new StepNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
