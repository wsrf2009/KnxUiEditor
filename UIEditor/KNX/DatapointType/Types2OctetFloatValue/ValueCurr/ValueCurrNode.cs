using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueCurr
{
    class ValueCurrNode:Types2OctetFloatValueNode
    {
        public ValueCurrNode()
        {
            this.KNXSubNumber = DPST_21;
            this.Name = "current (mA)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueCurrNode nodeType = new ValueCurrNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
