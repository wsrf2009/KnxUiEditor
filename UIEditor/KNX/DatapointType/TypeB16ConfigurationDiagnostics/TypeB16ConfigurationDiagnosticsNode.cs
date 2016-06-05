using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeB16ConfigurationDiagnostics.DALIControlGearDiagnostic;

namespace UIEditor.KNX.DatapointType.TypeB16ConfigurationDiagnostics
{
    class TypeB16ConfigurationDiagnosticsNode:DatapointType
    {
        public TypeB16ConfigurationDiagnosticsNode()
        {
            this.KNXMainNumber = DPT_237;
            this.Name = "configuration/ diagnostics";
            this.Type = Structure.KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB16ConfigurationDiagnosticsNode nodeType = new TypeB16ConfigurationDiagnosticsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(DALIControlGearDiagnosticNode.GetTypeNode());

            return nodeType;
        }
    }
}
