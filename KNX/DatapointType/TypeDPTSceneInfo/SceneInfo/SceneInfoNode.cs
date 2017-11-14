using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeDPTSceneInfo.SceneInfo
{
    class SceneInfoNode:TypeDPTSceneInfoNode
    {
        public SceneInfoNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "scene information";
        }

        public static TreeNode GetTypeNode()
        {
            SceneInfoNode nodeType = new SceneInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
