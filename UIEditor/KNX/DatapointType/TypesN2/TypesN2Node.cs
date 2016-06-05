using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesN2.AlarmReaction;
using UIEditor.KNX.DatapointType.TypesN2.HVACPBAction;
using UIEditor.KNX.DatapointType.TypesN2.OnOffAction;
using UIEditor.KNX.DatapointType.TypesN2.UpDownAction;

namespace UIEditor.KNX.DatapointType.TypesN2
{
    class TypesN2Node:DatapointType
    {
        public TypesN2Node()
        {
            this.KNXMainNumber = DPT_23;
            this.Name = "2-bit set";
            this.Type = KNXDataType.Bit2;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesN2Node nodeType = new TypesN2Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(OnOffActionNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmReactionNode.GetTypeNode());
            nodeType.Nodes.Add(UpDownActionNode.GetTypeNode());
            nodeType.Nodes.Add(HVACPBActionNode.GetTypeNode());

            return nodeType;
        }
    }
}
