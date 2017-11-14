using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.LogicalFunction
{
    class LogicalFunctionNode : TypesB1Node
    {
        public LogicalFunctionNode()
        {
            this.KNXSubNumber = DPST_21;
            this.DPTName = "logical function";
        }

        public static TreeNode GetTypeNode()
        {
            LogicalFunctionNode nodeType = new LogicalFunctionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
