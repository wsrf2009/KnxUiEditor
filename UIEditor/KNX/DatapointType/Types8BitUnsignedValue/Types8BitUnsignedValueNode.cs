using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Angle;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.DecimalFactor;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.PercentU8;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Scaling;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Tariff;
using UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Value1Ucount;

namespace UIEditor.KNX.DatapointType.Types8BitUnsignedValue
{
    class Types8BitUnsignedValueNode:DatapointType
    {
        public Types8BitUnsignedValueNode()
        {
            this.KNXMainNumber = DPT_5;
            this.Name = "8-bit unsigned value";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types8BitUnsignedValueNode nodeType = new Types8BitUnsignedValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ScalingNode.GetTypeNode());
            nodeType.Nodes.Add(AngleNode.GetTypeNode());
            nodeType.Nodes.Add(PercentU8Node.GetTypeNode());
            nodeType.Nodes.Add(DecimalFactorNode.GetTypeNode());
            nodeType.Nodes.Add(TariffNode.GetTypeNode());
            nodeType.Nodes.Add(Value1UcountNode.GetTypeNode());

            return nodeType;
        }

        public static TreeNode GetAllActionNode()
        {
            Types8BitUnsignedValueNode nodeAction = new Types8BitUnsignedValueNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;

            nodeAction.Nodes.Add(ScalingNode.GetActionNode());

            return nodeAction;
        }
    }
}
