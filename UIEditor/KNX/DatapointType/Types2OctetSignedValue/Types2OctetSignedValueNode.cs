using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTime100Msec;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTime10Msec;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeHrs;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMin;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMsec;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeSec;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.PercentV16;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.RotationAngle;
using UIEditor.KNX.DatapointType.Types2OctetSignedValue.Value2Count;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue
{
    class Types2OctetSignedValueNode:DatapointType
    {
        public Types2OctetSignedValueNode()
        {
            this.KNXMainNumber = DPT_8;
            this.Name = "2-byte signed value";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types2OctetSignedValueNode nodeType = new Types2OctetSignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

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
