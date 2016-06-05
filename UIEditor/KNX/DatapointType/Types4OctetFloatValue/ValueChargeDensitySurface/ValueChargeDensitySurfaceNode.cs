using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueChargeDensitySurface
{
    class ValueChargeDensitySurfaceNode:Types4OctetFloatValueNode
    {
        public ValueChargeDensitySurfaceNode()
        {
            this.KNXSubNumber = DPST_12;
            this.Name = "flux density (C/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueChargeDensitySurfaceNode nodeType = new ValueChargeDensitySurfaceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
