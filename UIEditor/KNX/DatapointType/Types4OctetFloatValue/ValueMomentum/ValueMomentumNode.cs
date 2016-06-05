using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMomentum
{
    class ValueMomentumNode:Types4OctetFloatValueNode
    {
        public ValueMomentumNode()
        {
            this.KNXSubNumber = DPST_53;
            this.Name = "momentum (N/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMomentumNode nodeType = new ValueMomentumNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
