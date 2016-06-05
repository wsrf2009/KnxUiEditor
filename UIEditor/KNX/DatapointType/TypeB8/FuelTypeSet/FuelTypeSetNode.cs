using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.FuelTypeSet
{
    class FuelTypeSetNode:TypeB8Node
    {
        public FuelTypeSetNode()
        {
            this.KNXSubNumber = DPST_104;
            this.Name = "fuel type set";
        }

        public static TreeNode GetTypeNode()
        {
            FuelTypeSetNode nodeType = new FuelTypeSetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
