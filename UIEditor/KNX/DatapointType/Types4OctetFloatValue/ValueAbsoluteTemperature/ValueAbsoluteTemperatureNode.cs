using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAbsoluteTemperature
{
    class ValueAbsoluteTemperatureNode : Types4OctetFloatValueNode
    {
        public ValueAbsoluteTemperatureNode()
        {
            this.KNXSubNumber = DPST_69;
            this.Name = "temperature absolute (°C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAbsoluteTemperatureNode nodeType = new ValueAbsoluteTemperatureNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
