using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Invert
{
    class InvertNode : TypesB1Node
    {
        public InvertNode()
        {
            //this.Text = "1.012 invert";
            this.KNXSubNumber = DPST_2;
            this.Name = "invert";
        }

        public static TreeNode GetTypeNode()
        {
            InvertNode nodeType = new InvertNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
