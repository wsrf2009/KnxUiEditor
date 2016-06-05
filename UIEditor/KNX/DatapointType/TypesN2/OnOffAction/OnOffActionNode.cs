using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN2.OnOffAction
{
    class OnOffActionNode:TypesN2Node
    {
        public OnOffActionNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "on/off action";
        }

        public static TreeNode GetTypeNode()
        {
            OnOffActionNode nodeType = new OnOffActionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
