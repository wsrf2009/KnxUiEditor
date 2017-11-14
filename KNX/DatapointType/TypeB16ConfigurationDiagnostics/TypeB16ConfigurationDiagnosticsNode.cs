using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeB16ConfigurationDiagnostics.DALIControlGearDiagnostic;

namespace KNX.DatapointType.TypeB16ConfigurationDiagnostics
{
    class TypeB16ConfigurationDiagnosticsNode:DatapointType
    {
        public TypeB16ConfigurationDiagnosticsNode()
        {
            this.KNXMainNumber = DPT_237;
            this.DPTName = "configuration/ diagnostics";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeB16ConfigurationDiagnosticsNode nodeType = new TypeB16ConfigurationDiagnosticsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(DALIControlGearDiagnosticNode.GetTypeNode());

            return nodeType;
        }
    }
}
