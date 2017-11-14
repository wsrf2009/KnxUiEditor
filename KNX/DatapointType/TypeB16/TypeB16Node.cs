using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB16.ChannelActivation16;
using KNX.DatapointType.TypeB16.Media;
using KNX.DatapointType.TypeB16.StatusDHWC;
using KNX.DatapointType.TypeB16.StatusRHCC;

namespace KNX.DatapointType.TypeB16
{
    class TypeB16Node:DatapointType
    {
        public TypeB16Node()
        {
            this.KNXMainNumber = DPT_22;
            this.DPTName = "16-bit set";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB16Node nodeType = new TypeB16Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(StatusDHWCNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRHCCNode.GetTypeNode());
            nodeType.Nodes.Add(MediaNode.GetTypeNode());
            nodeType.Nodes.Add(ChannelActivation16Node.GetTypeNode());

            return nodeType;
        }
    }
}
