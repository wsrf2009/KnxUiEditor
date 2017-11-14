using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.ChangeoverMode
{
    class ChangeoverModeNode : TypesN8Node
    {
        public ChangeoverModeNode()
        {
            this.KNXSubNumber = DPST_107;
            this.DPTName = "changeover mode";
        }

        public static TreeNode GetTypeNode()
        {
            ChangeoverModeNode nodeType = new ChangeoverModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
