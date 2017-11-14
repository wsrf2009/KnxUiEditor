using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueForce
{
    class ValueForceNode:Types4OctetFloatValueNode
    {
        public ValueForceNode()
        {
            this.KNXSubNumber = DPST_32;
            this.DPTName = "force (N)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueForceNode nodeType = new ValueForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
