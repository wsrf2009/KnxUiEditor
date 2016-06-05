using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodMsec
{
    class TimePeriodMsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriodMsecNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "time (ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriodMsecNode nodeType = new TimePeriodMsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
