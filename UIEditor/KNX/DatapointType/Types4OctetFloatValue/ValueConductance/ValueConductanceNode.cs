using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueConductance
{
    class ValueConductanceNode : Types4OctetFloatValueNode
    {
        public ValueConductanceNode()
        {
            this.KNXSubNumber = DPST_15;
            this.Name = "conductance (S)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueConductanceNode nodeType = new ValueConductanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
