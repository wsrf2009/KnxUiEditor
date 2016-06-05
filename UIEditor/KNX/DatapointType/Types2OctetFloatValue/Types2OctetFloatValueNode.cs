using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.KelvinPerPercent;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.Power;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.PowerDensity;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.RainAmount;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueAirFlow;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueAirQuality;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueCurr;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueHumidity;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueLux;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValuePres;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTemp;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTempa;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTempd;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTempF;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTime1;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTime2;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueVolt;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueVolumeFlow;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueWsp;
using UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueWspkmh;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue
{
    class Types2OctetFloatValueNode:DatapointType
    {
        public Types2OctetFloatValueNode()
        {
            this.KNXMainNumber = DPT_9;
            this.Name = "2-byte float value";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types2OctetFloatValueNode nodeType = new Types2OctetFloatValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ValueTempNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTempdNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTempaNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLuxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueWspNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePresNode.GetTypeNode());
            nodeType.Nodes.Add(ValueHumidityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAirQualityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAirFlowNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTime1Node.GetTypeNode());
            nodeType.Nodes.Add(ValueTime2Node.GetTypeNode());
            nodeType.Nodes.Add(ValueVoltNode.GetTypeNode());
            nodeType.Nodes.Add(ValueCurrNode.GetTypeNode());
            nodeType.Nodes.Add(PowerDensityNode.GetTypeNode());
            nodeType.Nodes.Add(KelvinPerPercentNode.GetTypeNode());
            nodeType.Nodes.Add(PowerNode.GetTypeNode());
            nodeType.Nodes.Add(ValueVolumeFlowNode.GetTypeNode());
            nodeType.Nodes.Add(RainAmountNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTempFNode.GetTypeNode());
            nodeType.Nodes.Add(ValueWspkmhNode.GetTypeNode());

            return nodeType;
        }
    }
}
