using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTime2
{
    class ValueTime2Node:Types2OctetFloatValueNode
    {
        public ValueTime2Node()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "time (ms)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTime2Node nodeType = new ValueTime2Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
