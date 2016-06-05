using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Reset
{
    class ResetNode : TypesB1Node
    {
        public ResetNode()
        {
            this.KNXSubNumber = DPST_15;
            this.Name = "reset";
        }

        public static TreeNode GetTypeNode()
        {
            ResetNode nodeType = new ResetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
