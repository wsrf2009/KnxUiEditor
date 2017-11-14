using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.DamperMode
{
    class DamperModeNode:TypesN8Node
    {
        public DamperModeNode()
        {
            this.KNXSubNumber = DPST_109;
            this.DPTName = "damper mode";
        }

        public static TreeNode GetTypeNode()
        {
            DamperModeNode nodeType = new DamperModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
