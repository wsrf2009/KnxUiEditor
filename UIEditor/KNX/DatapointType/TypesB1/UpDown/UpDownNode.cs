using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.UpDown
{
    class UpDownNode : TypesB1Node
    {
        public UpDownNode()
        {
            //this.Text = "1.008 up/down";
            this.KNXSubNumber = DPST_8;
            this.Name = "up/down";
        }

        public static TreeNode GetTypeNode()
        {
            UpDownNode nodeType = new UpDownNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
