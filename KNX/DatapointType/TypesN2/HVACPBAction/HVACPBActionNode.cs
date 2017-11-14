using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN2.HVACPBAction
{
    class HVACPBActionNode:TypesN2Node
    {
        public HVACPBActionNode()
        {
            this.KNXSubNumber = DPST_102;
            this.DPTName = "HVAC push button action";
        }

        public static TreeNode GetTypeNode()
        {
            HVACPBActionNode nodeType = new HVACPBActionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
