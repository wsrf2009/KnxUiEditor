using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesB1.Ack;
using UIEditor.KNX.DatapointType.TypesB1.Alarm;
using UIEditor.KNX.DatapointType.TypesB1.BinaryValue;
using UIEditor.KNX.DatapointType.TypesB1.Bool;
using UIEditor.KNX.DatapointType.TypesB1.HeatCool;
using UIEditor.KNX.DatapointType.TypesB1.DimSendStyle;
using UIEditor.KNX.DatapointType.TypesB1.Enable;
using UIEditor.KNX.DatapointType.TypesB1.InputSource;
using UIEditor.KNX.DatapointType.TypesB1.Invert;
using UIEditor.KNX.DatapointType.TypesB1.LogicalFunction;
using UIEditor.KNX.DatapointType.TypesB1.Occupancy;
using UIEditor.KNX.DatapointType.TypesB1.OpenClose;
using UIEditor.KNX.DatapointType.TypesB1.Ramp;
using UIEditor.KNX.DatapointType.TypesB1.Reset;
using UIEditor.KNX.DatapointType.TypesB1.SceneAB;
using UIEditor.KNX.DatapointType.TypesB1.ShutterBlindsMode;
using UIEditor.KNX.DatapointType.TypesB1.Start;
using UIEditor.KNX.DatapointType.TypesB1.State;
using UIEditor.KNX.DatapointType.TypesB1.Step;
using UIEditor.KNX.DatapointType.TypesB1.Switch;
using UIEditor.KNX.DatapointType.TypesB1.Trigger;
using UIEditor.KNX.DatapointType.TypesB1.UpDown;
using UIEditor.KNX.DatapointType.TypesB1.WindowDoor;

namespace UIEditor.KNX.DatapointType.TypesB1
{
    class TypesB1Node : DatapointType
    {
        public TypesB1Node()
        {
            this.KNXMainNumber = DPT_1;
            this.Name = "1-bit";
            this.Type = KNXDataType.Bit1;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesB1Node nodeType = new TypesB1Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(SwitchNode.GetTypeNode());
            nodeType.Nodes.Add(BoolNode.GetTypeNoe());
            nodeType.Nodes.Add(EnableNode.GetTypeNode());
            nodeType.Nodes.Add(RampNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmNode.GetTypeNode());
            nodeType.Nodes.Add(BinaryValueNode.GetTypeNode());
            nodeType.Nodes.Add(StepNode.GetTypeNode());
            nodeType.Nodes.Add(UpDownNode.GetTypeNode());
            nodeType.Nodes.Add(OpenCloseNode.GetTypeNode());
            nodeType.Nodes.Add(StartNode.GetTypeNode());
            nodeType.Nodes.Add(StateNode.GetTypeNode());
            nodeType.Nodes.Add(InvertNode.GetTypeNode());
            nodeType.Nodes.Add(DimSendStyleNode.GetTypeNode());
            nodeType.Nodes.Add(InputSourceNode.GetTypeNode());
            nodeType.Nodes.Add(ResetNode.GetTypeNode());
            nodeType.Nodes.Add(AckNode.GetTypeNode());
            nodeType.Nodes.Add(TriggerNode.GetTypeNode());
            nodeType.Nodes.Add(OccupancyNode.GetTypeNode());
            nodeType.Nodes.Add(WindowDoorNode.GetTypeNode());
            nodeType.Nodes.Add(LogicalFunctionNode.GetTypeNode());
            nodeType.Nodes.Add(SceneABNode.GetTypeNode());
            nodeType.Nodes.Add(ShutterBlindsModeNode.GetTypeNode());
            nodeType.Nodes.Add(HeatCoolNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            TypesB1Node nodeAction = new TypesB1Node();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            nodeAction.Nodes.Add(SwitchNode.GetActionNode());

            return nodeAction;
        }
    }
}
