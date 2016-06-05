using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.BeaufortWindForceScale
{
    class BeaufortWindForceScaleNode:TypesN8Node
    {
        public BeaufortWindForceScaleNode()
        {
            this.KNXSubNumber = DPST_14;
            this.Name = "wind force scale (0..12)";
        }

        public static TreeNode GetTypeNode()
        {
            BeaufortWindForceScaleNode nodeType = new BeaufortWindForceScaleNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
