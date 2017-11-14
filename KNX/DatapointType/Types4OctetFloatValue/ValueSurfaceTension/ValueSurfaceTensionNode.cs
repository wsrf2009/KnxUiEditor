using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueSurfaceTension
{
    class ValueSurfaceTensionNode:Types4OctetFloatValueNode
    {
        public ValueSurfaceTensionNode()
        {
            this.KNXSubNumber = DPST_67;
            this.DPTName = "surface tension (N/m)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueSurfaceTensionNode nodeType = new ValueSurfaceTensionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
