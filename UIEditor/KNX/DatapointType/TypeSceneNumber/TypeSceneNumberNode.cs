using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeSceneNumber.SceneNumber;

namespace UIEditor.KNX.DatapointType.TypeSceneNumber
{
    class TypeSceneNumberNode:DatapointType
    {
        public TypeSceneNumberNode()
        {
            this.KNXMainNumber = DPT_17;
            this.Name = "scene number";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeSceneNumberNode nodeType = new TypeSceneNumberNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(SceneNumberNode.GetTypeNode());

            return nodeType;
        }
    }
}
