using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueLuminousFlux
{
    class ValueLuminousFluxNode:Types4OctetFloatValueNode
    {
        public ValueLuminousFluxNode()
        {
            this.KNXSubNumber = DPST_42;
            this.DPTName = "luminous flux (lm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLuminousFluxNode nodeType = new ValueLuminousFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
