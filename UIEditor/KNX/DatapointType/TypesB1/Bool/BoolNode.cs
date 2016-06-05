using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Bool
{
    class BoolNode : TypesB1Node
    {
        public BoolNode()
        {
            //this.Text = "1.002 boolean";
            this.KNXSubNumber = DPST_2;
            this.Name = "boolean";
        }

        public static TreeNode GetTypeNoe()
        {
            BoolNode nodeType = new BoolNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
