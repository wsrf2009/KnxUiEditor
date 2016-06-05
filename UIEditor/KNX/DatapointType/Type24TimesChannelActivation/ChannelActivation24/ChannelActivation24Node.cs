using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Type24TimesChannelActivation.ChannelActivation24
{
    class ChannelActivation24Node:Type24TimesChannelActivationNode
    {
        public ChannelActivation24Node()
        {
            this.KNXSubNumber = DPST_1010;
            this.Name = "activation state 0..23";
        }

        public static TreeNode GetTypeNode()
        {
            ChannelActivation24Node nodeType = new ChannelActivation24Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
