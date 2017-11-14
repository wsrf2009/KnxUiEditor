using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueVolt
{
    class ValueVoltNode:Types2OctetFloatValueNode
    {
        public ValueVoltNode()
        {
            this.KNXSubNumber = DPST_20;
            this.DPTName = "voltage (mV)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVoltNode nodeType = new ValueVoltNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
