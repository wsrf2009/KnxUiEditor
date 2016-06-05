using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDPTSceneControl.SceneControl;

namespace UIEditor.KNX.DatapointType.TypeDPTSceneControl
{
    class TypeDPTSceneControlNode:DatapointType
    {
        public TypeDPTSceneControlNode()
        {
            this.KNXMainNumber = DPT_18;
            this.Name = "scene control";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTSceneControlNode nodeType = new TypeDPTSceneControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(SceneControlNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            TypeDPTSceneControlNode nodeAction = new TypeDPTSceneControlNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            nodeAction.Nodes.Add(SceneControlNode.GetActionNode());

            return nodeAction;
        }
    }
}
