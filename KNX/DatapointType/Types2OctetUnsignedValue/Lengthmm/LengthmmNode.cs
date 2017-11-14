using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.Lengthmm
{
    class LengthmmNode:Types2OctetUnsignedValueNode
    {
        public LengthmmNode()
        {
            this.KNXSubNumber = DPST_11;
            this.DPTName = "length (mm)";
        }

        public static TreeNode GetTypeNode()
        {
            LengthmmNode nodeType = new LengthmmNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
