using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.Start
{
    class StartNode : TypesB1Node
    {
        public StartNode()
        {
            //this.Text = "1.010 start/stop";
            this.KNXSubNumber = DPST_10;
            this.DPTName = "start/stop";
        }

        public static TreeNode GetTypeNode()
        {
            StartNode nodeType = new StartNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
