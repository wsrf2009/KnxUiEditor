using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.LogicalFunction
{
    class LogicalFunctionNode : TypesB1Node
    {
        public LogicalFunctionNode()
        {
            this.KNXSubNumber = DPST_21;
            this.Name = "logical function";
        }

        public static TreeNode GetTypeNode()
        {
            LogicalFunctionNode nodeType = new LogicalFunctionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
