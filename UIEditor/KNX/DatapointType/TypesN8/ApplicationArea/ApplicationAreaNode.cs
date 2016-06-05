using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ApplicationArea
{
    class ApplicationAreaNode:TypesN8Node
    {
        public ApplicationAreaNode()
        {
            this.KNXSubNumber = DPST_6;
            this.Name = "light application area";
        }

        public static TreeNode GetTypeNode()
        {
            ApplicationAreaNode nodeType = new ApplicationAreaNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
