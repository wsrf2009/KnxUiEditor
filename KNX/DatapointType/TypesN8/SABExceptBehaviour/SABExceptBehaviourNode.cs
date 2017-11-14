using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.SABExceptBehaviour
{
    class SABExceptBehaviourNode:TypesN8Node
    {
        public SABExceptBehaviourNode()
        {
            this.KNXSubNumber = DPST_801;
            this.DPTName = "SAB except behavior";
        }

        public static TreeNode GetTypeNode()
        {
            SABExceptBehaviourNode nodeType = new SABExceptBehaviourNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
