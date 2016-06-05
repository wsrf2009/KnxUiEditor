using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.ChannelActivation8
{
    class ChannelActivation8Node:TypeB8Node
    {
        public ChannelActivation8Node()
        {
            this.KNXSubNumber = DPST_1010;
            this.Name = "channel activation for 8 channels";
        }

        public static TreeNode GetTypeNode()
        {
            ChannelActivation8Node nodeType = new ChannelActivation8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
