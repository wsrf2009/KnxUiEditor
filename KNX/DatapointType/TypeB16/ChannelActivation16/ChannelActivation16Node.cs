using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB16.ChannelActivation16
{
    class ChannelActivation16Node:TypeB16Node
    {
        public ChannelActivation16Node()
        {
            this.KNXSubNumber = DPST_1010;
            this.DPTName = "channel activation for 16 channels";
        }

        public static TreeNode GetTypeNode()
        {
            ChannelActivation16Node nodeType = new ChannelActivation16Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
