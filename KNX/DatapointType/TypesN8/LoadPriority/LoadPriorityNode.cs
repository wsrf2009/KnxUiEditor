using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.LoadPriority
{
    class LoadPriorityNode:TypesN8Node
    {
        public LoadPriorityNode()
        {
            this.KNXSubNumber = DPST_104;
            this.DPTName = "load priority";
        }

        public static TreeNode GetTypeNode()
        {
            LoadPriorityNode nodeType = new LoadPriorityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
