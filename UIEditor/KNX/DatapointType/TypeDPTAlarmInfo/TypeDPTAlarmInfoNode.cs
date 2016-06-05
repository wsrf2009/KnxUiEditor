using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeDPTAlarmInfo.AlarmInfo;

namespace UIEditor.KNX.DatapointType.TypeDPTAlarmInfo
{
    class TypeDPTAlarmInfoNode:DatapointType
    {
        public TypeDPTAlarmInfoNode()
        {
            this.KNXMainNumber = DPT_219;
            this.Name = "alarm info";
            this.Type = KNXDataType.Bit48;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDPTAlarmInfoNode nodeType = new TypeDPTAlarmInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(AlarmInfoNode.GetTypeNode());

            return nodeType;
        }
    }
}
