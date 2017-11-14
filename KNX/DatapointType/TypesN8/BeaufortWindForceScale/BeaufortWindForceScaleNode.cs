using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.BeaufortWindForceScale
{
    class BeaufortWindForceScaleNode:TypesN8Node
    {
        public BeaufortWindForceScaleNode()
        {
            this.KNXSubNumber = DPST_14;
            this.DPTName = "wind force scale (0..12)";
        }

        public static TreeNode GetTypeNode()
        {
            BeaufortWindForceScaleNode nodeType = new BeaufortWindForceScaleNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
