using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagnetization
{
    class ValueMagnetizationNode:Types4OctetFloatValueNode
    {
        public ValueMagnetizationNode()
        {
            this.KNXSubNumber = DPST_49;
            this.Name = "magnetization (A/m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagnetizationNode nodeType = new ValueMagnetizationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
