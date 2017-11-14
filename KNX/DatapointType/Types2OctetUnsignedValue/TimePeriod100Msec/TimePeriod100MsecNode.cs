using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod100Msec
{
    class TimePeriod100MsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriod100MsecNode()
        {
            this.KNXSubNumber = DPST_4;
            this.DPTName = "time (100 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriod100MsecNode nodeType = new TimePeriod100MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
