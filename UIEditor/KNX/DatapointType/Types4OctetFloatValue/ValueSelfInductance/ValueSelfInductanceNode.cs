using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSelfInductance
{
    class ValueSelfInductanceNode:Types4OctetFloatValueNode
    {
        public ValueSelfInductanceNode()
        {
            this.KNXSubNumber = DPST_62;
            this.Name = "self inductance (H)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueSelfInductanceNode nodeType = new ValueSelfInductanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
