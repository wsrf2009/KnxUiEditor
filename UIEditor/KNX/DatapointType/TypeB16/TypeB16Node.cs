using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeB16.ChannelActivation16;
using UIEditor.KNX.DatapointType.TypeB16.Media;
using UIEditor.KNX.DatapointType.TypeB16.StatusDHWC;
using UIEditor.KNX.DatapointType.TypeB16.StatusRHCC;

namespace UIEditor.KNX.DatapointType.TypeB16
{
    class TypeB16Node:DatapointType
    {
        public TypeB16Node()
        {
            this.KNXMainNumber = DPT_22;
            this.Name = "16-bit set";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB16Node nodeType = new TypeB16Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(StatusDHWCNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRHCCNode.GetTypeNode());
            nodeType.Nodes.Add(MediaNode.GetTypeNode());
            nodeType.Nodes.Add(ChannelActivation16Node.GetTypeNode());

            return nodeType;
        }
    }
}
