using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.AddInfoTypes
{
    class AddInfoTypesNode:TypesN8Node
    {
        public AddInfoTypesNode()
        {
            this.KNXSubNumber = DPST_1001;
            this.Name = "additional information type";
        }

        public static TreeNode GetTypeNode()
        {
            AddInfoTypesNode nodeType = new AddInfoTypesNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
