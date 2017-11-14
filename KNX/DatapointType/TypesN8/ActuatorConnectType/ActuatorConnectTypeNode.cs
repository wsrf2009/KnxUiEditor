using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.ActuatorConnectType
{
    class ActuatorConnectTypeNode:TypesN8Node
    {
        public ActuatorConnectTypeNode()
        {
            this.KNXSubNumber = DPST_20;
            this.DPTName = "actuator connect type";
        }

        public static TreeNode GetTypeNode()
        {
            ActuatorConnectTypeNode nodeType = new ActuatorConnectTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
