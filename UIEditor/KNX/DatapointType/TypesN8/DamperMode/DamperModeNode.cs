using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.DamperMode
{
    class DamperModeNode:TypesN8Node
    {
        public DamperModeNode()
        {
            this.KNXSubNumber = DPST_109;
            this.Name = "damper mode";
        }

        public static TreeNode GetTypeNode()
        {
            DamperModeNode nodeType = new DamperModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
