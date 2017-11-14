using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueTime1
{
    class ValueTime1Node:Types2OctetFloatValueNode
    {
        public ValueTime1Node()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "time (s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTime1Node nodeType = new ValueTime1Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
