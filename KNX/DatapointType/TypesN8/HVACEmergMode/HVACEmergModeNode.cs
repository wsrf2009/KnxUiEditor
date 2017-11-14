using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.HVACEmergMode
{
    class HVACEmergModeNode:TypesN8Node
    {
        public HVACEmergModeNode()
        {
            this.KNXSubNumber = DPST_106;
            this.DPTName = "HVAC emergency mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACEmergModeNode nodeType = new HVACEmergModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
