using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueAirFlow
{
    class ValueAirFlowNode:Types2OctetFloatValueNode
    {
        public ValueAirFlowNode()
        {
            this.KNXSubNumber = DPST_9;
            this.DPTName = "air flow (m³/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAirFlowNode nodeType = new ValueAirFlowNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
