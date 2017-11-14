using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointAction;

namespace KNX.DatapointType.TypeDPTSceneControl.SceneControl
{
    class SceneControlNode:TypeDPTSceneControlNode
    {
        public SceneControlNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "scene control";
        }

        public static TreeNode GetTypeNode()
        {
            SceneControlNode nodeType = new SceneControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }

        public static TreeNode GetActionNode()
        {
            SceneControlNode nodeAction = new SceneControlNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;

            DatapointActionNode actionScene1 = new DatapointActionNode();
            actionScene1.ActionName = actionScene1.Text = KNXResMang.GetString("Scene1");
            actionScene1.Value = 0;

            DatapointActionNode actionScene2 = new DatapointActionNode();
            actionScene2.ActionName = actionScene2.Text = KNXResMang.GetString("Scene2");
            actionScene2.Value = 1;

            DatapointActionNode actionScene3 = new DatapointActionNode();
            actionScene3.ActionName = actionScene3.Text = KNXResMang.GetString("Scene3");
            actionScene3.Value = 2;

            nodeAction.Nodes.Add(actionScene1);
            nodeAction.Nodes.Add(actionScene2);
            nodeAction.Nodes.Add(actionScene3);

            return nodeAction;
        }
    }
}
