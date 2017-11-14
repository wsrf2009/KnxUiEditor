using KNX.DatapointType.Type24TimesChannelActivation.ChannelActivation24;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type24TimesChannelActivation
{
    class Type24TimesChannelActivationNode:DatapointType
    {
        public Type24TimesChannelActivationNode()
        {
            this.KNXMainNumber = DPT_30;
            this.DPTName = "24 times channel activation";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type24TimesChannelActivationNode nodeType = new Type24TimesChannelActivationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(ChannelActivation24Node.GetTypeNode());

            return nodeType;
        }
    }
}
