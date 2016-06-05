using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.State
{
    class StateNode : TypesB1Node
    {
        public StateNode()
        {
            //this.Text = "1.011 state";
            this.KNXSubNumber = DPST_11;
            this.Name = "state";
        }

        public static TreeNode GetTypeNode()
        {
            StateNode nodeType = new StateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
