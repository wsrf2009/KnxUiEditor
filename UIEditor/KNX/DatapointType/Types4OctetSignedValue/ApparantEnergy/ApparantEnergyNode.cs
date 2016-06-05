using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetSignedValue.ApparantEnergy
{
    class ApparantEnergyNode:Types4OctetSignedValueNode
    {
        public ApparantEnergyNode()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "apparant energy (VAh)";
        }

        public static TreeNode GetTypeNode()
        {
            ApparantEnergyNode nodeType = new ApparantEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
