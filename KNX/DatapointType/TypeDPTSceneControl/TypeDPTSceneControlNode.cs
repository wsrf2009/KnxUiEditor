using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDPTSceneControl.SceneControl;

namespace KNX.DatapointType.TypeDPTSceneControl
{
    class TypeDPTSceneControlNode:DatapointType
    {
        public TypeDPTSceneControlNode()
        {
            this.KNXMainNumber = DPT_18;
            this.DPTName = "scene control";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTSceneControlNode nodeType = new TypeDPTSceneControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(SceneControlNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            TypeDPTSceneControlNode nodeAction = new TypeDPTSceneControlNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;

            nodeAction.Nodes.Add(SceneControlNode.GetActionNode());

            return nodeAction;
        }
    }
}
