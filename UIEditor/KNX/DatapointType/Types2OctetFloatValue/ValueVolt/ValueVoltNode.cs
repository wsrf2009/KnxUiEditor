using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueVolt
{
    class ValueVoltNode:Types2OctetFloatValueNode
    {
        public ValueVoltNode()
        {
            this.KNXSubNumber = DPST_20;
            this.Name = "voltage (mV)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVoltNode nodeType = new ValueVoltNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
