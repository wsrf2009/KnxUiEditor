
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeTime.TimeOfDay;

namespace KNX.DatapointType.TypeTime
{
    class TypeTimeNode:DatapointType
    {
        public TypeTimeNode()
        {
            this.KNXMainNumber = DPT_10;
            this.DPTName = "time";
            this.Type = KNXDataType.Bit24;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeTimeNode nodeType = new TypeTimeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(TimeOfDayNode.GetTypeNode());

            return nodeType;
        }
    }
}
