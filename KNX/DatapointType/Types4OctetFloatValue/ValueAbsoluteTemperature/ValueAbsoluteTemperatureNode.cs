using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAbsoluteTemperature
{
    class ValueAbsoluteTemperatureNode : Types4OctetFloatValueNode
    {
        public ValueAbsoluteTemperatureNode()
        {
            this.KNXSubNumber = DPST_69;
            this.DPTName = "temperature absolute (°C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAbsoluteTemperatureNode nodeType = new ValueAbsoluteTemperatureNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
