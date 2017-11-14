using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueChargeDensityVolume
{
    class ValueChargeDensityVolumeNode:Types4OctetFloatValueNode
    {
        public ValueChargeDensityVolumeNode()
        {
            this.KNXSubNumber = DPST_13;
            this.DPTName = "charge density (C/m³)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueChargeDensityVolumeNode nodeType = new ValueChargeDensityVolumeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
