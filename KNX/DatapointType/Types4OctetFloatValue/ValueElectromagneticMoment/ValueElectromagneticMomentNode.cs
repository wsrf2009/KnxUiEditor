using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectromagneticMoment
{
    class ValueElectromagneticMomentNode:Types4OctetFloatValueNode
    {
        public ValueElectromagneticMomentNode()
        {
            this.KNXSubNumber = DPST_29;
            this.DPTName = "electromagnetic moment (Am²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectromagneticMomentNode nodeType = new ValueElectromagneticMomentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
