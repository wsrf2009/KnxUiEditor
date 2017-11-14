using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueHeatFlowRate
{
    class ValueHeatFlowRateNode:Types4OctetFloatValueNode
    {
        public ValueHeatFlowRateNode()
        {
            this.KNXSubNumber = DPST_36;
            this.DPTName = "heat flow rate (W)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHeatFlowRateNode nodeType = new ValueHeatFlowRateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
