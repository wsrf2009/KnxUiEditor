using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.LightActuatorErrorInfo
{
    class LightActuatorErrorInfoNode : TypeB8Node
    {
        public LightActuatorErrorInfoNode()
        {
            this.KNXSubNumber = DPST_601;
            this.Name = "lighting actuator error information";
        }

        public static TreeNode GetTypeNode()
        {
            LightActuatorErrorInfoNode nodeType = new LightActuatorErrorInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
