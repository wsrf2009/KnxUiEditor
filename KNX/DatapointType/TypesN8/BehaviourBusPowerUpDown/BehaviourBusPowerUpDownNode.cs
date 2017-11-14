using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.BehaviourBusPowerUpDown
{
    class BehaviourBusPowerUpDownNode:TypesN8Node
    {
        public BehaviourBusPowerUpDownNode()
        {
            this.KNXSubNumber = DPST_601;
            this.DPTName = "behavior bus power up/down";
        }

        public static TreeNode GetTypeNode()
        {
            BehaviourBusPowerUpDownNode nodeType = new BehaviourBusPowerUpDownNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
