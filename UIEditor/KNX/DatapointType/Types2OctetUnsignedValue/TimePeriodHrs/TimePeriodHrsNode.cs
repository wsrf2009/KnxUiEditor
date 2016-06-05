using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodHrs
{
    class TimePeriodHrsNode:Types2OctetUnsignedValueNode
    {
        public TimePeriodHrsNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "time (h)";
        }

        public static TreeNode GetTypeNode()
        {
            TimePeriodHrsNode nodeType = new TimePeriodHrsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
