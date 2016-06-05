using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeDPTAlarmInfo.AlarmInfo
{
    class AlarmInfoNode:TypeDPTAlarmInfoNode
    {
        public AlarmInfoNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "alarm info";
        }

        public static TreeNode GetTypeNode()
        {
            AlarmInfoNode nodeType = new AlarmInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
