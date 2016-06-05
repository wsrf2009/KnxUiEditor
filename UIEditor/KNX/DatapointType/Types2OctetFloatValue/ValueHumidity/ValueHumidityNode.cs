using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueHumidity
{
    class ValueHumidityNode:Types2OctetFloatValueNode
    {
        public ValueHumidityNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "humidity (%)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueHumidityNode nodeType = new ValueHumidityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
