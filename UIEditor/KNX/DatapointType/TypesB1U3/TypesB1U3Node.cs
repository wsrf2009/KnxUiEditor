using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesB1U3.ControlBlinds;
using UIEditor.KNX.DatapointType.TypesB1U3.ControlDimming;

namespace UIEditor.KNX.DatapointType.TypesB1U3
{
    class TypesB1U3Node : DatapointType
    {
        public TypesB1U3Node()
        {
            this.KNXMainNumber = DPT_3;
            this.Name = "3-bit controlled";
            this.Type = KNXDataType.Bit4;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesB1U3Node nodeType = new TypesB1U3Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ControlDimmingNode.GetTypeNode());
            nodeType.Nodes.Add(ControlBlindsNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            TypesB1U3Node nodeAction = new TypesB1U3Node();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            nodeAction.Nodes.Add(ControlDimmingNode.GetActionNode());

            return nodeAction;
        }
    }
}
