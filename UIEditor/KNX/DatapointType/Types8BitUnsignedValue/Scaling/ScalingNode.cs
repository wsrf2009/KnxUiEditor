using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Scaling
{
    class ScalingNode:Types8BitUnsignedValueNode
    {
        public ScalingNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "percentage (0..100%)";
        }

        public static TreeNode GetTypeNode()
        {
            ScalingNode nodeType = new ScalingNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }

        public static TreeNode GetActionNode()
        {
            ScalingNode nodeAction = new ScalingNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            DatapointActionNode actionAdjustTo30per = new DatapointActionNode();
            actionAdjustTo30per.Name = actionAdjustTo30per.Text = ResourceMng.GetString("AdjustTo30per");
            actionAdjustTo30per.Value = 76;

            DatapointActionNode actionAdjustTo60per = new DatapointActionNode();
            actionAdjustTo60per.Name = actionAdjustTo60per.Text = ResourceMng.GetString("AdjustTo60per");
            actionAdjustTo60per.Value = 153;

            DatapointActionNode actionAdjustTo90per = new DatapointActionNode();
            actionAdjustTo90per.Name = actionAdjustTo90per.Text = ResourceMng.GetString("AdjustTo90per");
            actionAdjustTo90per.Value = 229;


            nodeAction.Nodes.Add(actionAdjustTo30per);
            nodeAction.Nodes.Add(actionAdjustTo60per);
            nodeAction.Nodes.Add(actionAdjustTo90per);

            return nodeAction;
        }
    }
}
