using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeDPTDateTime.DateTime
{
    class DateTimeNode:TypeDPTDateTimeNode
    {
        public DateTimeNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "date time";
        }

        public static TreeNode GetTypeNode()
        {
            DateTimeNode nodeType = new DateTimeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
