using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeSceneNumber.SceneNumber
{
    class SceneNumberNode:TypeSceneNumberNode
    {
        public SceneNumberNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "scene number";
        }

        public static TreeNode GetTypeNode()
        {
            SceneNumberNode nodeType = new SceneNumberNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
