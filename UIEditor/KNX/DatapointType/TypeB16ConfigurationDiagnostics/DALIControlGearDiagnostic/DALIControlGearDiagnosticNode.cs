using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB16ConfigurationDiagnostics.DALIControlGearDiagnostic
{
    class DALIControlGearDiagnosticNode:TypeB16ConfigurationDiagnosticsNode
    {
        public DALIControlGearDiagnosticNode()
        {
            this.KNXSubNumber = DPST_600;
            this.Name = "diagnostic value";
        }

        public static TreeNode GetTypeNode()
        {
            DALIControlGearDiagnosticNode nodeType = new DALIControlGearDiagnosticNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
