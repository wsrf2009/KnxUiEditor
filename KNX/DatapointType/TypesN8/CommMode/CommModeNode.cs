using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.CommMode
{
    class CommModeNode : TypesN8Node
    {
        public CommModeNode()
        {
            this.KNXSubNumber = DPST_1000;
            this.DPTName = "communication mode";
        }

        public static TreeNode GetTypeNode()
        {
            CommModeNode nodeType = new CommModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
