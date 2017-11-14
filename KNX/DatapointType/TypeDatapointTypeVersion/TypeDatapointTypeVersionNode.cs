using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeDatapointTypeVersion.Version;

namespace KNX.DatapointType.TypeDatapointTypeVersion
{
    class TypeDatapointTypeVersionNode:DatapointType
    {
        public TypeDatapointTypeVersionNode()
        {
            this.KNXMainNumber = DPT_217;
            this.DPTName = "datapoint type version";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeDatapointTypeVersionNode nodeType = new TypeDatapointTypeVersionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(VersionNode.GetTypeNode());

            return nodeType;
        }
    }
}
