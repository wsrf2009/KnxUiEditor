using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.AlarmControl
{
    class AlarmControlNode : TypesB2Node
    {
        public AlarmControlNode()
        {
            this.KNXSubNumber = DPST_5;
            this.DPTName = "alarm control";
        }

        public static TreeNode GetTypeNode()
        {
            AlarmControlNode nodeType = new AlarmControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
