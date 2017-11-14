using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMass
{
    class ValueMassNode:Types4OctetFloatValueNode
    {
        public ValueMassNode()
        {
            this.KNXSubNumber = DPST_51;
            this.DPTName = "mass (kg)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMassNode nodeType = new ValueMassNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
