using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.KNX.DatapointType.TypesB1U3.ControlDimming
{
    class ControlDimmingNode:TypesB1U3Node
    {
        public ControlDimmingNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "dimming control";
        }

        public static TreeNode GetTypeNode()
        {
            ControlDimmingNode nodeType = new ControlDimmingNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }

        public static TreeNode GetActionNode()
        {
            ControlDimmingNode nodeAction = new ControlDimmingNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            DatapointActionNode actionBrighter25per = new DatapointActionNode();
            actionBrighter25per.Name = actionBrighter25per.Text = ResourceMng.GetString("Brighter25per");
            actionBrighter25per.Value = 0x0B;

            DatapointActionNode actionBrighter50per = new DatapointActionNode();
            actionBrighter50per.Name = actionBrighter50per.Text = ResourceMng.GetString("Brighter50per");
            actionBrighter50per.Value = 0x0A;

            DatapointActionNode actionBrighter100per = new DatapointActionNode();
            actionBrighter100per.Name = actionBrighter100per.Text = ResourceMng.GetString("Brighter100per");
            actionBrighter100per.Value = 0x09;

            DatapointActionNode actionDim25per = new DatapointActionNode();
            actionDim25per.Name = actionDim25per.Text = ResourceMng.GetString("Dim25per");
            actionDim25per.Value = 0x03;

            DatapointActionNode actionDim50per = new DatapointActionNode();
            actionDim50per.Name = actionDim50per.Text = ResourceMng.GetString("Dim50per");
            actionDim50per.Value = 0x02;

            DatapointActionNode actionDim100per = new DatapointActionNode();
            actionDim100per.Name = actionDim100per.Text = ResourceMng.GetString("Dim100per");
            actionDim100per.Value = 0x01;

            nodeAction.Nodes.Add(actionBrighter25per);
            nodeAction.Nodes.Add(actionBrighter50per);
            nodeAction.Nodes.Add(actionBrighter100per);
            nodeAction.Nodes.Add(actionDim25per);
            nodeAction.Nodes.Add(actionDim50per);
            nodeAction.Nodes.Add(actionDim100per);

            return nodeAction;
        }
    }
}
