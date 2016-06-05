using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.StartSynchronization
{
    class StartSynchronizationNode:TypesN8Node
    {
        public StartSynchronizationNode()
        {
            this.KNXSubNumber = DPST_122;
            this.Name = "start syncronization type";
        }

        public static TreeNode GetTypeNode()
        {
            StartSynchronizationNode nodeType = new StartSynchronizationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
