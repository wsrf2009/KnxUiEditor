using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.BehaviourLockUnlock
{
    class BehaviourLockUnlockNode:TypesN8Node
    {
        public BehaviourLockUnlockNode()
        {
            this.KNXSubNumber = DPST_600;
            this.DPTName = "behavior lock/unlock";
        }

        public static TreeNode GetTypeNode()
        {
            BehaviourLockUnlockNode nodeType = new BehaviourLockUnlockNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
