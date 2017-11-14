using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueVolume
{
    class ValueVolumeNode:Types4OctetFloatValueNode
    {
        public ValueVolumeNode()
        {
            this.KNXSubNumber = DPST_76;
            this.DPTName = "volume (m³)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVolumeNode nodeType = new ValueVolumeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
