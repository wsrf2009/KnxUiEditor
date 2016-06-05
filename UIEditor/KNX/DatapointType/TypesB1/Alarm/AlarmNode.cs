using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Alarm
{
    class AlarmNode : TypesB1Node
    {
        public AlarmNode()
        {
            //this.Text = "1.005 alarm";
            this.KNXSubNumber = DPST_5;
            this.Name = "alarm";
        }

        public static TreeNode GetTypeNode()
        {
            AlarmNode nodeType = new AlarmNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
