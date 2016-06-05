using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB2.StartControl
{
    class StartControlNode:TypesB2Node
    {
        public StartControlNode()
        {
            this.KNXSubNumber = DPST_10;
            this.Name = "start control";
        }

        public static TreeNode GetTypeNode()
        {
            StartControlNode nodeType = new StartControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
