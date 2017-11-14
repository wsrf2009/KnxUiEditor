using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.ValueWspkmh
{
    class ValueWspkmhNode:Types2OctetFloatValueNode
    {
        public ValueWspkmhNode()
        {
            this.KNXSubNumber = DPST_28;
            this.DPTName = "wind speed (km/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueWspkmhNode nodeType = new ValueWspkmhNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
