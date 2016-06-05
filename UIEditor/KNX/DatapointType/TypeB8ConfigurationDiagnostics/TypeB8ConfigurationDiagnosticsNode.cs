using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeB8ConfigurationDiagnostics.DALIDiagnostics;

namespace UIEditor.KNX.DatapointType.TypeB8ConfigurationDiagnostics
{
    class TypeB8ConfigurationDiagnosticsNode:DatapointType
    {
        public TypeB8ConfigurationDiagnosticsNode()
        {
            this.KNXMainNumber = DPT_238;
            this.Name = "configuration/ diagnostics";
            this.Type = Structure.KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB8ConfigurationDiagnosticsNode nodeType = new TypeB8ConfigurationDiagnosticsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(DALIDiagnosticsNode.GetTypeNode());

            return nodeType;
        }
    }
}
