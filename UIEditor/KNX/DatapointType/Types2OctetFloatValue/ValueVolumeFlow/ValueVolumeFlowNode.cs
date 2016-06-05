using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueVolumeFlow
{
    class ValueVolumeFlowNode:Types2OctetFloatValueNode
    {
        public ValueVolumeFlowNode()
        {
            this.KNXSubNumber = DPST_25;
            this.Name = "volume flow (l/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVolumeFlowNode nodeType = new ValueVolumeFlowNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
