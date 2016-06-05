using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod10Msec
{
    class TimePeriod10MsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriod10MsecNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "time (10 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriod10MsecNode nodeType = new TimePeriod10MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
