using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesCharacterSet.Char88591
{
    class Char88591Node:TypesCharacterSetNode
    {
        public Char88591Node()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "character (ISO 8859-1)";
        }

        public static TreeNode GetTypeNode()
        {
            Char88591Node nodeType = new Char88591Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
