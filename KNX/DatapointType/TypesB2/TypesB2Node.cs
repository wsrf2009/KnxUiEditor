
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypesB2.AlarmControl;
using KNX.DatapointType.TypesB2.BinaryValueControl;
using KNX.DatapointType.TypesB2.BoolControl;
using KNX.DatapointType.TypesB2.Direction1Control;
using KNX.DatapointType.TypesB2.Direction2Control;
using KNX.DatapointType.TypesB2.EnableControl;
using KNX.DatapointType.TypesB2.InvertControl;
using KNX.DatapointType.TypesB2.RampControl;
using KNX.DatapointType.TypesB2.StartControl;
using KNX.DatapointType.TypesB2.StateControl;
using KNX.DatapointType.TypesB2.StepControl;
using KNX.DatapointType.TypesB2.SwitchControl;

namespace KNX.DatapointType.TypesB2
{
    class TypesB2Node : DatapointType
    {
        public TypesB2Node()
        {
            //this.Text = "2.* 1-bit controlled";
            this.KNXMainNumber = DPT_2;
            this.DPTName = "1-bit controlled";
            this.Type = KNXDataType.Bit2;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesB2Node nodeType = new TypesB2Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(SwitchControlNode.GetTypeNode());
            nodeType.Nodes.Add(BoolControlNode.GetTypeNode());
            nodeType.Nodes.Add(EnableControlNode.GetTypeNode());
            nodeType.Nodes.Add(RampControlNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmControlNode.GetTypeNode());
            nodeType.Nodes.Add(BinaryValueControlNode.GetTypeNode());
            nodeType.Nodes.Add(StepControlNode.GetTypeNode());
            nodeType.Nodes.Add(Direction1ControlNode.GetTypeNode());
            nodeType.Nodes.Add(Direction2ControlNode.GetTypeNode());
            nodeType.Nodes.Add(StartControlNode.GetTypeNode());
            nodeType.Nodes.Add(StateControlNode.GetTypeNode());
            nodeType.Nodes.Add(InvertControlNode.GetTypeNode());

            return nodeType;
        }
    }
}
