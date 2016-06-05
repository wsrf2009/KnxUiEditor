using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN2.AlarmReaction
{
    class AlarmReactionNode:TypesN2Node
    {
        public AlarmReactionNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "alarm reaction";
        }

        public static TreeNode GetTypeNode()
        {
            AlarmReactionNode nodeType = new AlarmReactionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
