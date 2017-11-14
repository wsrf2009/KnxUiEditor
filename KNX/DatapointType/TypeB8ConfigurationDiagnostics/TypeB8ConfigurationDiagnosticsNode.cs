using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB8ConfigurationDiagnostics.DALIDiagnostics;

namespace KNX.DatapointType.TypeB8ConfigurationDiagnostics
{
    class TypeB8ConfigurationDiagnosticsNode:DatapointType
    {
        public TypeB8ConfigurationDiagnosticsNode()
        {
            this.KNXMainNumber = DPT_238;
            this.DPTName = "configuration/ diagnostics";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB8ConfigurationDiagnosticsNode nodeType = new TypeB8ConfigurationDiagnosticsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(DALIDiagnosticsNode.GetTypeNode());

            return nodeType;
        }
    }
}
