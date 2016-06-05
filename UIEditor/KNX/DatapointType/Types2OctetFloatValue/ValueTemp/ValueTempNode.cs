using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTemp
{
    class ValueTempNode:Types2OctetFloatValueNode
    {
        public ValueTempNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "temperature (°C)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTempNode nodeType = new ValueTempNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
