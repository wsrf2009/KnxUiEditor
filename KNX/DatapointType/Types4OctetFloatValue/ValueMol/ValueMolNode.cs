using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMol
{
    class ValueMolNode:Types4OctetFloatValueNode
    {
        public ValueMolNode()
        {
            this.KNXSubNumber = DPST_4;
            this.DPTName = "amount of substance (mol)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMolNode nodeType = new ValueMolNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
