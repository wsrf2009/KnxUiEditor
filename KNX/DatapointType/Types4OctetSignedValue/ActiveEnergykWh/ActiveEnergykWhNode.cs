using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetSignedValue.ActiveEnergykWh
{
    class ActiveEnergykWhNode:Types4OctetSignedValueNode
    {
        public ActiveEnergykWhNode()
        {
            this.KNXSubNumber = DPST_13;
            this.DPTName = "active energy (kWh)";
        }

        public static TreeNode GetTypeNode()
        {
            ActiveEnergykWhNode nodeType = new ActiveEnergykWhNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
