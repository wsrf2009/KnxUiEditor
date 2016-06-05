using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.LightControlMode
{
    class LightControlModeNode:TypesN8Node
    {
        public LightControlModeNode()
        {
            this.KNXSubNumber = DPST_604;
            this.Name = "light control mode";
        }

        public static TreeNode GetTypeNode()
        {
            LightControlModeNode nodeType = new LightControlModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
