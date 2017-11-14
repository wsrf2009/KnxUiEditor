using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodMsec
{
    class TimePeriodMsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriodMsecNode()
        {
            this.KNXSubNumber = DPST_2;
            this.DPTName = "time (ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriodMsecNode nodeType = new TimePeriodMsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
