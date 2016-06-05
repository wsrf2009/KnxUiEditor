using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDPTSceneInfo.SceneInfo;

namespace UIEditor.KNX.DatapointType.TypeDPTSceneInfo
{
    class TypeDPTSceneInfoNode:DatapointType
    {
        public TypeDPTSceneInfoNode()
        {
            this.KNXMainNumber = DPT_26;
            this.Name = "8-bit set";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTSceneInfoNode nodeType = new TypeDPTSceneInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(SceneInfoNode.GetTypeNode());

            return nodeType;
        }
    }
}
