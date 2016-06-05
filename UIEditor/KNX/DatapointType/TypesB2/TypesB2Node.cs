using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesB2.AlarmControl;
using UIEditor.KNX.DatapointType.TypesB2.BinaryValueControl;
using UIEditor.KNX.DatapointType.TypesB2.BoolControl;
using UIEditor.KNX.DatapointType.TypesB2.Direction1Control;
using UIEditor.KNX.DatapointType.TypesB2.Direction2Control;
using UIEditor.KNX.DatapointType.TypesB2.EnableControl;
using UIEditor.KNX.DatapointType.TypesB2.InvertControl;
using UIEditor.KNX.DatapointType.TypesB2.RampControl;
using UIEditor.KNX.DatapointType.TypesB2.StartControl;
using UIEditor.KNX.DatapointType.TypesB2.StateControl;
using UIEditor.KNX.DatapointType.TypesB2.StepControl;
using UIEditor.KNX.DatapointType.TypesB2.SwitchControl;

namespace UIEditor.KNX.DatapointType.TypesB2
{
    class TypesB2Node : DatapointType
    {
        public TypesB2Node()
        {
            //this.Text = "2.* 1-bit controlled";
            this.KNXMainNumber = DPT_2;
            this.Name = "1-bit controlled";
            this.Type = KNXDataType.Bit2;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesB2Node nodeType = new TypesB2Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

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
