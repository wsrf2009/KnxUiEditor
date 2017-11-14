using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMagnetization
{
    class ValueMagnetizationNode:Types4OctetFloatValueNode
    {
        public ValueMagnetizationNode()
        {
            this.KNXSubNumber = DPST_49;
            this.DPTName = "magnetization (A/m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagnetizationNode nodeType = new ValueMagnetizationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
