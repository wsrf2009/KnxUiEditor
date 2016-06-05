using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.DALIFadeTime
{
    class DALIFadeTimeNode:TypesN8Node
    {
        public DALIFadeTimeNode()
        {
            this.KNXSubNumber = DPST_602;
            this.Name = "dali fade time";
        }

        public static TreeNode GetTypeNode()
        {
            DALIFadeTimeNode nodeType = new DALIFadeTimeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
