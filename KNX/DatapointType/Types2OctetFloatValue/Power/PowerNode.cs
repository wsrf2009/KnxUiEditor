using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.Power
{
    class PowerNode:Types2OctetFloatValueNode
    {
        public PowerNode()
        {
            this.KNXSubNumber = DPST_24;
            this.DPTName = "power (kW)";
        }

        public static TreeNode GetTypeNode()
        {
            PowerNode nodeType = new PowerNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
