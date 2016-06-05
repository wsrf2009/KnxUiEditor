using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.Brightness;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.Lengthmm;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.PropDataType;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod100Msec;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriod10Msec;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodHrs;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodMin;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodMsec;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.TimePeriodSec;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.UElCurrentmA;
using UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.Value2Ucount;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue
{
    class Types2OctetUnsignedValueNode:DatapointType
    {
        public Types2OctetUnsignedValueNode()
        {
            this.KNXMainNumber = DPT_7;
            this.Name = "2-byte unsigned value";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types2OctetUnsignedValueNode nodeType = new Types2OctetUnsignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(Value2UcountNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriodMsecNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriod10MsecNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriod100MsecNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriodSecNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriodMinNode.GetTypeNode());
            nodeType.Nodes.Add(TimePeriodHrsNode.GetTypeNode());
            nodeType.Nodes.Add(PropDataTypeNode.GetTypeNode());
            nodeType.Nodes.Add(LengthmmNode.GetTypeNode());
            nodeType.Nodes.Add(UElCurrentmANode.GetTypeNode());
            nodeType.Nodes.Add(BrightnessNode.GetTypeNode());

            return nodeType;
        }
    }
}
