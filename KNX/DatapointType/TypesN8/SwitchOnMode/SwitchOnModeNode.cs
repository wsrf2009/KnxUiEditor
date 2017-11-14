using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.SwitchOnMode
{
    class SwitchOnModeNode:TypesN8Node
    {
        public SwitchOnModeNode()
        {
            this.KNXSubNumber = DPST_608;
            this.DPTName = "switch on mode";
        }

        public static TreeNode GetTypeNode()
        {
            SwitchOnModeNode nodeType = new SwitchOnModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
