using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Type24TimesChannelActivation.ChannelActivation24;

namespace UIEditor.KNX.DatapointType.Type24TimesChannelActivation
{
    class Type24TimesChannelActivationNode:DatapointType
    {
        public Type24TimesChannelActivationNode()
        {
            this.KNXMainNumber = DPT_30;
            this.Name = "24 times channel activation";
            this.Type = Structure.KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type24TimesChannelActivationNode nodeType = new Type24TimesChannelActivationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ChannelActivation24Node.GetTypeNode());

            return nodeType;
        }
    }
}
