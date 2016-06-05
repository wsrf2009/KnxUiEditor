using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8ConfigurationDiagnostics.DALIDiagnostics
{
    class DALIDiagnosticsNode:TypeB8ConfigurationDiagnosticsNode
    {
        public DALIDiagnosticsNode()
        {
            this.KNXSubNumber = DPST_600;
            this.Name = "diagnostic value";
        }

        public static TreeNode GetTypeNode()
        {
            DALIDiagnosticsNode nodeType = new DALIDiagnosticsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
