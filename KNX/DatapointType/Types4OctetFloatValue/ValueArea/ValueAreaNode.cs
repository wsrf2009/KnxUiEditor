using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueArea
{
    class ValueAreaNode:Types4OctetFloatValueNode
    {
        public ValueAreaNode()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "area (m*m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAreaNode nodeType = new ValueAreaNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
