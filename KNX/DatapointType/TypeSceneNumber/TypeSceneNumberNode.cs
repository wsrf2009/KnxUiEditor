using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeSceneNumber.SceneNumber;

namespace KNX.DatapointType.TypeSceneNumber
{
    class TypeSceneNumberNode:DatapointType
    {
        public TypeSceneNumberNode()
        {
            this.KNXMainNumber = DPT_17;
            this.DPTName = "scene number";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeSceneNumberNode nodeType = new TypeSceneNumberNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(SceneNumberNode.GetTypeNode());

            return nodeType;
        }
    }
}
