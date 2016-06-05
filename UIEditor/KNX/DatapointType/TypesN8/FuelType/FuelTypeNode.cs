using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.FuelType
{
    class FuelTypeNode:TypesN8Node
    {
        public FuelTypeNode()
        {
            this.KNXSubNumber = DPST_100;
            this.Name = "fuel type";
        }
        public static TreeNode GetTypeNode()
        {
            FuelTypeNode nodeType = new FuelTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
