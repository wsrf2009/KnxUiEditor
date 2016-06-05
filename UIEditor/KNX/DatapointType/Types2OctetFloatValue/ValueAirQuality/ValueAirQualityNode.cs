using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueAirQuality
{
    class ValueAirQualityNode:Types2OctetFloatValueNode
    {
        public ValueAirQualityNode()
        {
            this.KNXSubNumber = DPST_8;
            this.Name = "parts/million (ppm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAirQualityNode nodeType = new ValueAirQualityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
