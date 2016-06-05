using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueAirFlow
{
    class ValueAirFlowNode:Types2OctetFloatValueNode
    {
        public ValueAirFlowNode()
        {
            this.KNXSubNumber = DPST_9;
            this.Name = "air flow (m³/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAirFlowNode nodeType = new ValueAirFlowNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
