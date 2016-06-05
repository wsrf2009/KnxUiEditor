using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularMomentum
{
    class ValueAngularMomentumNode:Types4OctetFloatValueNode
    {
        public ValueAngularMomentumNode()
        {
            this.KNXSubNumber = DPST_8;
            this.Name = "angular momentum (Js)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngularMomentumNode nodeType = new ValueAngularMomentumNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
