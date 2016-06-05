using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ActuatorConnectType
{
    class ActuatorConnectTypeNode:TypesN8Node
    {
        public ActuatorConnectTypeNode()
        {
            this.KNXSubNumber = DPST_20;
            this.Name = "actuator connect type";
        }

        public static TreeNode GetTypeNode()
        {
            ActuatorConnectTypeNode nodeType = new ActuatorConnectTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
