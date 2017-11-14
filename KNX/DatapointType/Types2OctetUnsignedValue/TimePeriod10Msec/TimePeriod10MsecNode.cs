using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod10Msec
{
    class TimePeriod10MsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriod10MsecNode()
        {
            this.KNXSubNumber = DPST_3;
            this.DPTName = "time (10 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriod10MsecNode nodeType = new TimePeriod10MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
