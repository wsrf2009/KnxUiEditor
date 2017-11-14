using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueHumidity
{
    class ValueHumidityNode:Types2OctetFloatValueNode
    {
        public ValueHumidityNode()
        {
            this.KNXSubNumber = DPST_7;
            this.DPTName = "humidity (%)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHumidityNode nodeType = new ValueHumidityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
