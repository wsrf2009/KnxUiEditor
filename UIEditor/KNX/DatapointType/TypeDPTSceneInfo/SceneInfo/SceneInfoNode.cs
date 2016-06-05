using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeDPTSceneInfo.SceneInfo
{
    class SceneInfoNode:TypeDPTSceneInfoNode
    {
        public SceneInfoNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "scene information";
        }

        public static TreeNode GetTypeNode()
        {
            SceneInfoNode nodeType = new SceneInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
