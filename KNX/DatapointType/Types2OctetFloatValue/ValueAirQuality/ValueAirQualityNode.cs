using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueAirQuality
{
    class ValueAirQualityNode:Types2OctetFloatValueNode
    {
        public ValueAirQualityNode()
        {
            this.KNXSubNumber = DPST_8;
            this.DPTName = "parts/million (ppm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAirQualityNode nodeType = new ValueAirQualityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
