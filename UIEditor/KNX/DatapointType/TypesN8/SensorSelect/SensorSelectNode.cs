using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.SensorSelect
{
    class SensorSelectNode:TypesN8Node
    {
        public SensorSelectNode()
        {
            this.KNXSubNumber = DPST_17;
            this.Name = "sensor mode";
        }

        public static TreeNode GetTypeNode()
        {
            SensorSelectNode nodeType = new SensorSelectNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
