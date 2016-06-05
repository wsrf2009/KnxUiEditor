using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ChangeoverMode
{
    class ChangeoverModeNode : TypesN8Node
    {
        public ChangeoverModeNode()
        {
            this.KNXSubNumber = DPST_107;
            this.Name = "changeover mode";
        }

        public static TreeNode GetTypeNode()
        {
            ChangeoverModeNode nodeType = new ChangeoverModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
