using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodMin
{
    class TimePeriodMinNode:Types2OctetUnsignedValueNode
    {
        public TimePeriodMinNode()
        {
            this.KNXSubNumber = DPST_6;
            this.DPTName = "time (min)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriodMinNode nodeType = new TimePeriodMinNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
