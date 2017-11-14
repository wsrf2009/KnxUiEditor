using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeDate.Date
{
    class DateNode:TypeDateNode
    {
        public DateNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "date";
        }

        public static TreeNode GetTypeNode()
        {
            DateNode nodeType = new DateNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
