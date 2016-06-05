using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.Occupancy
{
    class OccupancyNode : TypesB1Node
    {
        public OccupancyNode()
        {
            this.KNXSubNumber = DPST_18;
            this.Name = "occupancy";
        }

        public static TreeNode GetTypeNode()
        {
            OccupancyNode nodeType = new OccupancyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
