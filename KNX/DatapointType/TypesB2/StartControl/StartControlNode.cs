using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.StartControl
{
    class StartControlNode:TypesB2Node
    {
        public StartControlNode()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "start control";
        }

        public static TreeNode GetTypeNode()
        {
            StartControlNode nodeType = new StartControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
