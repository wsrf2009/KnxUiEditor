using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.SABExceptBehaviour
{
    class SABExceptBehaviourNode:TypesN8Node
    {
        public SABExceptBehaviourNode()
        {
            this.KNXSubNumber = DPST_801;
            this.Name = "SAB except behavior";
        }

        public static TreeNode GetTypeNode()
        {
            SABExceptBehaviourNode nodeType = new SABExceptBehaviourNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
