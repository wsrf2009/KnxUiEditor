using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeTime.TimeOfDay
{
    class TimeOfDayNode:TypeTimeNode
    {
        public TimeOfDayNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "time of day";
        }

        public static TreeNode GetTypeNode()
        {
            TimeOfDayNode nodeType = new TimeOfDayNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
