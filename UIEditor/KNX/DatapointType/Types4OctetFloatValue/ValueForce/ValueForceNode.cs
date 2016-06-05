using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueForce
{
    class ValueForceNode:Types4OctetFloatValueNode
    {
        public ValueForceNode()
        {
            this.KNXSubNumber = DPST_32;
            this.Name = "force (N)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueForceNode nodeType = new ValueForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
