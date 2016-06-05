using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.TimeDelay
{
    class TimeDelayNode:TypesN8Node
    {
        public TimeDelayNode()
        {
            this.KNXSubNumber = DPST_13;
            this.Name = "time delay";
        }

        public static TreeNode GetTypeNode()
        {
            TimeDelayNode nodeType = new TimeDelayNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
