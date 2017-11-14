
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypesB1U3.ControlBlinds;
using KNX.DatapointType.TypesB1U3.ControlDimming;

namespace KNX.DatapointType.TypesB1U3
{
    class TypesB1U3Node : DatapointType
    {
        public TypesB1U3Node()
        {
            this.KNXMainNumber = DPT_3;
            this.DPTName = "3-bit controlled";
            this.Type = KNXDataType.Bit4;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesB1U3Node nodeType = new TypesB1U3Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(ControlDimmingNode.GetTypeNode());
            nodeType.Nodes.Add(ControlBlindsNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            TypesB1U3Node nodeAction = new TypesB1U3Node();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;

            nodeAction.Nodes.Add(ControlDimmingNode.GetActionNode());

            return nodeAction;
        }
    }
}
