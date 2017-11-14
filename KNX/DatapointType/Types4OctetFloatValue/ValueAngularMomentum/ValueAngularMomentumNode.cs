using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueAngularMomentum
{
    class ValueAngularMomentumNode:Types4OctetFloatValueNode
    {
        public ValueAngularMomentumNode()
        {
            this.KNXSubNumber = DPST_8;
            this.DPTName = "angular momentum (Js)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngularMomentumNode nodeType = new ValueAngularMomentumNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
