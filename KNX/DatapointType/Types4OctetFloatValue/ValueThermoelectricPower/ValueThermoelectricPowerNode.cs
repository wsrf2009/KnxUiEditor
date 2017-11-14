using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueThermoelectricPower
{
    class ValueThermoelectricPowerNode : Types4OctetFloatValueNode
    {
        public ValueThermoelectricPowerNode()
        {
            this.KNXSubNumber = DPST_73;
            this.DPTName = "thermoelectric power (V/K)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueThermoelectricPowerNode nodeType = new ValueThermoelectricPowerNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
