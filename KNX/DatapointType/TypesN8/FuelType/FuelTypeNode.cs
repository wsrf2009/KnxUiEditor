using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.FuelType
{
    class FuelTypeNode:TypesN8Node
    {
        public FuelTypeNode()
        {
            this.KNXSubNumber = DPST_100;
            this.DPTName = "fuel type";
        }
        public static TreeNode GetTypeNode()
        {
            FuelTypeNode nodeType = new FuelTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
