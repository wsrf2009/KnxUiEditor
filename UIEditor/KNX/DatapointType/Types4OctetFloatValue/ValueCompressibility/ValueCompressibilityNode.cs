using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueCompressibility
{
    class ValueCompressibilityNode:Types4OctetFloatValueNode
    {
        public ValueCompressibilityNode()
        {
            this.KNXSubNumber = DPST_14;
            this.Name = "compressibility (m²/N)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueCompressibilityNode nodeType = new ValueCompressibilityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
