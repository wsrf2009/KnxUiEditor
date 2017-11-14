
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.Types2OctetFloatValue.KelvinPerPercent;
using KNX.DatapointType.Types2OctetFloatValue.Power;
using KNX.DatapointType.Types2OctetFloatValue.PowerDensity;
using KNX.DatapointType.Types2OctetFloatValue.RainAmount;
using KNX.DatapointType.Types2OctetFloatValue.ValueAirFlow;
using KNX.DatapointType.Types2OctetFloatValue.ValueAirQuality;
using KNX.DatapointType.Types2OctetFloatValue.ValueCurr;
using KNX.DatapointType.Types2OctetFloatValue.ValueHumidity;
using KNX.DatapointType.Types2OctetFloatValue.ValueLux;
using KNX.DatapointType.Types2OctetFloatValue.ValuePres;
using KNX.DatapointType.Types2OctetFloatValue.ValueTemp;
using KNX.DatapointType.Types2OctetFloatValue.ValueTempa;
using KNX.DatapointType.Types2OctetFloatValue.ValueTempd;
using KNX.DatapointType.Types2OctetFloatValue.ValueTempF;
using KNX.DatapointType.Types2OctetFloatValue.ValueTime1;
using KNX.DatapointType.Types2OctetFloatValue.ValueTime2;
using KNX.DatapointType.Types2OctetFloatValue.ValueVolt;
using KNX.DatapointType.Types2OctetFloatValue.ValueVolumeFlow;
using KNX.DatapointType.Types2OctetFloatValue.ValueWsp;
using KNX.DatapointType.Types2OctetFloatValue.ValueWspkmh;

namespace KNX.DatapointType.Types2OctetFloatValue
{
    class Types2OctetFloatValueNode:DatapointType
    {
        public Types2OctetFloatValueNode()
        {
            this.KNXMainNumber = DPT_9;
            this.DPTName = "2-byte float value";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types2OctetFloatValueNode nodeType = new Types2OctetFloatValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

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
