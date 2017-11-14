using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMin
{
    class DeltaTimeMinNode:Types2OctetSignedValueNode
    {
        public DeltaTimeMinNode()
        {
            this.KNXSubNumber = DPST_6;
            this.DPTName = "time lag (min)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTimeMinNode nodeType = new DeltaTimeMinNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
