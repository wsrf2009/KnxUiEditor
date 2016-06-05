using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.SABBehaviourLockUnlock
{
    class SABBehaviourLockUnlockNode:TypesN8Node
    {
        public SABBehaviourLockUnlockNode()
        {
            this.KNXSubNumber = DPST_802;
            this.Name = "SAB behavior on lock/unlock";
        }

        public static TreeNode GetTypeNode()
        {
            SABBehaviourLockUnlockNode nodeType = new SABBehaviourLockUnlockNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
