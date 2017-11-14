using KNX.DatapointAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types8BitUnsignedValue.Scaling
{
    class ScalingNode:Types8BitUnsignedValueNode
    {
        public ScalingNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "percentage (0..100%)";
        }

        public static TreeNode GetTypeNode()
        {
            ScalingNode nodeType = new ScalingNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }

        public static TreeNode GetActionNode()
        {
            ScalingNode nodeAction = new ScalingNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;

            DatapointActionNode actionAdjustTo0per = new DatapointActionNode();
            actionAdjustTo0per.ActionName = actionAdjustTo0per.Text = KNXResMang.GetString("AdjustTo0per");
            actionAdjustTo0per.Value = 0;

            DatapointActionNode actionAdjustTo10per = new DatapointActionNode();
            actionAdjustTo10per.ActionName = actionAdjustTo10per.Text = KNXResMang.GetString("AdjustTo10per");
            actionAdjustTo10per.Value = 26;

            DatapointActionNode actionAdjustTo20per = new DatapointActionNode();
            actionAdjustTo20per.ActionName = actionAdjustTo20per.Text = KNXResMang.GetString("AdjustTo20per");
            actionAdjustTo20per.Value = 51;

            DatapointActionNode actionAdjustTo30per = new DatapointActionNode();
            actionAdjustTo30per.ActionName = actionAdjustTo30per.Text = KNXResMang.GetString("AdjustTo30per");
            actionAdjustTo30per.Value = 77;

            DatapointActionNode actionAdjustTo40per = new DatapointActionNode();
            actionAdjustTo40per.ActionName = actionAdjustTo40per.Text = KNXResMang.GetString("AdjustTo40per");
            actionAdjustTo40per.Value = 102;

            DatapointActionNode actionAdjustTo50per = new DatapointActionNode();
            actionAdjustTo50per.ActionName = actionAdjustTo50per.Text = KNXResMang.GetString("AdjustTo50per");
            actionAdjustTo50per.Value = 128;

            DatapointActionNode actionAdjustTo60per = new DatapointActionNode();
            actionAdjustTo60per.ActionName = actionAdjustTo60per.Text = KNXResMang.GetString("AdjustTo60per");
            actionAdjustTo60per.Value = 153;

            DatapointActionNode actionAdjustTo70per = new DatapointActionNode();
            actionAdjustTo70per.ActionName = actionAdjustTo70per.Text = KNXResMang.GetString("AdjustTo70per");
            actionAdjustTo70per.Value = 179;

            DatapointActionNode actionAdjustTo80per = new DatapointActionNode();
            actionAdjustTo80per.ActionName = actionAdjustTo80per.Text = KNXResMang.GetString("AdjustTo80per");
            actionAdjustTo80per.Value = 204;

            DatapointActionNode actionAdjustTo90per = new DatapointActionNode();
            actionAdjustTo90per.ActionName = actionAdjustTo90per.Text = KNXResMang.GetString("AdjustTo90per");
            actionAdjustTo90per.Value = 230;

            DatapointActionNode actionAdjustTo100per = new DatapointActionNode();
            actionAdjustTo100per.ActionName = actionAdjustTo100per.Text = KNXResMang.GetString("AdjustTo100per");
            actionAdjustTo100per.Value = 255;

            nodeAction.Nodes.Add(actionAdjustTo0per);
            nodeAction.Nodes.Add(actionAdjustTo10per);
            nodeAction.Nodes.Add(actionAdjustTo20per);
            nodeAction.Nodes.Add(actionAdjustTo30per);
            nodeAction.Nodes.Add(actionAdjustTo40per);
            nodeAction.Nodes.Add(actionAdjustTo50per);
            nodeAction.Nodes.Add(actionAdjustTo60per);
            nodeAction.Nodes.Add(actionAdjustTo70per);
            nodeAction.Nodes.Add(actionAdjustTo80per);
            nodeAction.Nodes.Add(actionAdjustTo90per);
            nodeAction.Nodes.Add(actionAdjustTo100per);

            return nodeAction;
        }
    }
}
