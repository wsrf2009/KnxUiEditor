using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.HVACContrMode
{
    class HVACContrModeNode:TypesN8Node
    {
        public HVACContrModeNode()
        {
            this.KNXSubNumber = DPST_105;
            this.DPTName = "HVAC control mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACContrModeNode nodeType = new HVACContrModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
