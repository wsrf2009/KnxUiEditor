using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.HeatCool
{
    class HeatCoolNode:TypesB1Node
    {
        public HeatCoolNode()
        {
            this.KNXSubNumber = DPST_100;
            this.DPTName = "cooling/heating";
        }

        public static TreeNode GetTypeNode()
        {
            HeatCoolNode nodeType = new HeatCoolNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
