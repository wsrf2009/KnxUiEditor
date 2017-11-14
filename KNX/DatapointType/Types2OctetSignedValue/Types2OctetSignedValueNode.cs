using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTime100Msec;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTime10Msec;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTimeHrs;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMin;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMsec;
using KNX.DatapointType.Types2OctetSignedValue.DeltaTimeSec;
using KNX.DatapointType.Types2OctetSignedValue.PercentV16;
using KNX.DatapointType.Types2OctetSignedValue.RotationAngle;
using KNX.DatapointType.Types2OctetSignedValue.Value2Count;

namespace KNX.DatapointType.Types2OctetSignedValue
{
    class Types2OctetSignedValueNode:DatapointType
    {
        public Types2OctetSignedValueNode()
        {
            this.KNXMainNumber = DPT_8;
            this.DPTName = "2-byte signed value";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types2OctetSignedValueNode nodeType = new Types2OctetSignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(Value2CountNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTimeMsecNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTime10MsecNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTime100MsecNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTimeSecNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTimeMinNode.GetTypeNode());
            nodeType.Nodes.Add(DeltaTimeHrsNode.GetTypeNode());
            nodeType.Nodes.Add(PercentV16Node.GetTypeNode());
            nodeType.Nodes.Add(RotationAngleNode.GetTypeNode());

            return nodeType;
        }
    }
}
