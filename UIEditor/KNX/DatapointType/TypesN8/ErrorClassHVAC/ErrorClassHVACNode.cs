using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ErrorClassHVAC
{
    class ErrorClassHVACNode:TypesN8Node
    {
        public ErrorClassHVACNode()
        {
            this.KNXSubNumber = DPST_12;
            this.Name = "HVAC error class";
        }

        public static TreeNode GetTypeNode()
        {
            ErrorClassHVACNode nodeType = new ErrorClassHVACNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
