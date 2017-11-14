using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.ShutterBlindsMode
{
    class ShutterBlindsModeNode : TypesB1Node
    {
        public ShutterBlindsModeNode()
        {
            this.KNXSubNumber = DPST_23;
            this.DPTName = "schutter/blinds mode";
        }

        public static TreeNode GetTypeNode()
        {
            ShutterBlindsModeNode nodeType = new ShutterBlindsModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
