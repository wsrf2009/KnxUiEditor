using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValuePres
{
    class ValuePresNode:Types2OctetFloatValueNode
    {
        public ValuePresNode()
        {
            this.KNXSubNumber = DPST_6;
            this.DPTName = "pressure (Pa)";
        }

        public static TreeNode GetTypeNode()
        {
            ValuePresNode nodeType = new ValuePresNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
