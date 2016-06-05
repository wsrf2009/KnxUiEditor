using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueResistance
{
    class ValueResistanceNode:Types4OctetFloatValueNode
    {
        public ValueResistanceNode()
        {
            this.KNXSubNumber = DPST_60;
            this.Name = "resistance (Ω)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueResistanceNode nodeType = new ValueResistanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
