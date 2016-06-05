using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectromagneticMoment
{
    class ValueElectromagneticMomentNode:Types4OctetFloatValueNode
    {
        public ValueElectromagneticMomentNode()
        {
            this.KNXSubNumber = DPST_29;
            this.Name = "electromagnetic moment (Am²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectromagneticMomentNode nodeType = new ValueElectromagneticMomentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
