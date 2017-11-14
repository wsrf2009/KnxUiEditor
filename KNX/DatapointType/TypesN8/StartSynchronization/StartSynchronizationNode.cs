using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.StartSynchronization
{
    class StartSynchronizationNode:TypesN8Node
    {
        public StartSynchronizationNode()
        {
            this.KNXSubNumber = DPST_122;
            this.DPTName = "start syncronization type";
        }

        public static TreeNode GetTypeNode()
        {
            StartSynchronizationNode nodeType = new StartSynchronizationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
