using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueTemperatureDifference
{
    class ValueTemperatureDifferenceNode:Types4OctetFloatValueNode
    {
        public ValueTemperatureDifferenceNode()
        {
            this.KNXSubNumber = DPST_70;
            this.Name = "temperature difference (K)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTemperatureDifferenceNode nodeType = new ValueTemperatureDifferenceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
