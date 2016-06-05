using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.SCLOMode
{
    class SCLOModeNode:TypesN8Node
    {
        public SCLOModeNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "SCLO mode";
        }

        public static TreeNode GetTypeNode()
        {
            SCLOModeNode nodeType = new SCLOModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
