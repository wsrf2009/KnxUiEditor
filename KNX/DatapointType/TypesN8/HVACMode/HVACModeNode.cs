using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.HVACMode
{
    class HVACModeNode:TypesN8Node
    {
        public HVACModeNode()
        {
            this.KNXSubNumber = DPST_102;
            this.DPTName = "HVAC mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACModeNode nodeType = new HVACModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
