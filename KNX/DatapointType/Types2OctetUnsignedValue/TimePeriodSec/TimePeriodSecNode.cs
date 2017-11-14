using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodSec
{
    class TimePeriodSecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriodSecNode()
        {
            this.KNXSubNumber = DPST_5;
            this.DPTName = "time (s)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriodSecNode nodeType = new TimePeriodSecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
