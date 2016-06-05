using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod100Msec
{
    class TimePeriod100MsecNode:Types2OctetUnsignedValueNode
    {
        public TimePeriod100MsecNode()
        {
            this.KNXSubNumber = DPST_4;
            this.Name = "time (100 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriod100MsecNode nodeType = new TimePeriod100MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
