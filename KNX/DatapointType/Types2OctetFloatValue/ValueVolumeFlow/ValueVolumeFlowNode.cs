using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueVolumeFlow
{
    class ValueVolumeFlowNode:Types2OctetFloatValueNode
    {
        public ValueVolumeFlowNode()
        {
            this.KNXSubNumber = DPST_25;
            this.DPTName = "volume flow (l/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVolumeFlowNode nodeType = new ValueVolumeFlowNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
