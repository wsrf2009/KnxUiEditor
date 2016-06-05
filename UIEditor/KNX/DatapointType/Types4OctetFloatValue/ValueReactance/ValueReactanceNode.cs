using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueReactance
{
    class ValueReactanceNode:Types4OctetFloatValueNode
    {
        public ValueReactanceNode()
        {
            this.KNXSubNumber = DPST_59;
            this.Name = "reactance (Ω)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueReactanceNode nodeType = new ValueReactanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
