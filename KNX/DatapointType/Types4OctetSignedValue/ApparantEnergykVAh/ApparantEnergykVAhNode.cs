using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetSignedValue.ApparantEnergykVAh
{
    class ApparantEnergykVAhNode : Types4OctetSignedValueNode
    {
        public ApparantEnergykVAhNode()
        {
            this.KNXSubNumber = DPST_14;
            this.DPTName = "apparant energy (kVAh)";
        }

        public static TreeNode GetTypeNode()
        {
            ApparantEnergykVAhNode nodeType = new ApparantEnergykVAhNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
