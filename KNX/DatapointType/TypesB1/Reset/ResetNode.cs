using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.Reset
{
    class ResetNode : TypesB1Node
    {
        public ResetNode()
        {
            this.KNXSubNumber = DPST_15;
            this.DPTName = "reset";
        }

        public static TreeNode GetTypeNode()
        {
            ResetNode nodeType = new ResetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
