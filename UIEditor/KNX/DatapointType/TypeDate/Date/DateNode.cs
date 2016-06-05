using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeDate.Date
{
    class DateNode:TypeDateNode
    {
        public DateNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "date";
        }

        public static TreeNode GetTypeNode()
        {
            DateNode nodeType = new DateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
