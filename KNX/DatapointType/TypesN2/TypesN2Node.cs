using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypesN2.AlarmReaction;
using KNX.DatapointType.TypesN2.HVACPBAction;
using KNX.DatapointType.TypesN2.OnOffAction;
using KNX.DatapointType.TypesN2.UpDownAction;

namespace KNX.DatapointType.TypesN2
{
    class TypesN2Node:DatapointType
    {
        public TypesN2Node()
        {
            this.KNXMainNumber = DPT_23;
            this.DPTName = "2-bit set";
            this.Type = KNXDataType.Bit2;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesN2Node nodeType = new TypesN2Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(OnOffActionNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmReactionNode.GetTypeNode());
            nodeType.Nodes.Add(UpDownActionNode.GetTypeNode());
            nodeType.Nodes.Add(HVACPBActionNode.GetTypeNode());

            return nodeType;
        }
    }
}
