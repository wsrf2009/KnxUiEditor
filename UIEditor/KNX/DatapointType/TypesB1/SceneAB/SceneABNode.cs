using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.SceneAB
{
    class SceneABNode:TypesB1Node
    {
        public SceneABNode()
        {
            this.KNXSubNumber = DPST_22;
            this.Name = "scene";
        }

        public static TreeNode GetTypeNode()
        {
            SceneABNode nodeType = new SceneABNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
