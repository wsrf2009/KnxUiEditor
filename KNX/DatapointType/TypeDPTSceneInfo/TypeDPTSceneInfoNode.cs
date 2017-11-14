using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDPTSceneInfo.SceneInfo;

namespace KNX.DatapointType.TypeDPTSceneInfo
{
    class TypeDPTSceneInfoNode:DatapointType
    {
        public TypeDPTSceneInfoNode()
        {
            this.KNXMainNumber = DPT_26;
            this.DPTName = "8-bit set";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTSceneInfoNode nodeType = new TypeDPTSceneInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(SceneInfoNode.GetTypeNode());

            return nodeType;
        }
    }
}
