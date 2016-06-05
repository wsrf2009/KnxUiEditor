using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.HVACEmergMode
{
    class HVACEmergModeNode:TypesN8Node
    {
        public HVACEmergModeNode()
        {
            this.KNXSubNumber = DPST_106;
            this.Name = "HVAC emergency mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACEmergModeNode nodeType = new HVACEmergModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
