using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueCompressibility
{
    class ValueCompressibilityNode:Types4OctetFloatValueNode
    {
        public ValueCompressibilityNode()
        {
            this.KNXSubNumber = DPST_14;
            this.DPTName = "compressibility (m²/N)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueCompressibilityNode nodeType = new ValueCompressibilityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
