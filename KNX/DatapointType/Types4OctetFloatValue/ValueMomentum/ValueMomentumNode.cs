using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMomentum
{
    class ValueMomentumNode:Types4OctetFloatValueNode
    {
        public ValueMomentumNode()
        {
            this.KNXSubNumber = DPST_53;
            this.DPTName = "momentum (N/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMomentumNode nodeType = new ValueMomentumNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
