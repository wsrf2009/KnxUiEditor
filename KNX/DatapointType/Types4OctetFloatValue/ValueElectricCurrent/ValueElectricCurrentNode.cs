using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectricCurrent
{
    class ValueElectricCurrentNode:Types4OctetFloatValueNode
    {
        public ValueElectricCurrentNode()
        {
            this.KNXSubNumber = DPST_19;
            this.DPTName = "electric current (A)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricCurrentNode nodeType = new ValueElectricCurrentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
