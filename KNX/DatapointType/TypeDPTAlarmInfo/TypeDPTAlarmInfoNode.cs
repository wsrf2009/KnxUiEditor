using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDPTAlarmInfo.AlarmInfo;

namespace KNX.DatapointType.TypeDPTAlarmInfo
{
    class TypeDPTAlarmInfoNode:DatapointType
    {
        public TypeDPTAlarmInfoNode()
        {
            this.KNXMainNumber = DPT_219;
            this.DPTName = "alarm info";
            this.Type = KNXDataType.Bit48;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTAlarmInfoNode nodeType = new TypeDPTAlarmInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(AlarmInfoNode.GetTypeNode());

            return nodeType;
        }
    }
}
