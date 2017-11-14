using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeB16.Media
{
    class MediaNode:TypeB16Node
    {
        public MediaNode()
        {
            this.KNXSubNumber = DPST_1000;
            this.DPTName = "media";
        }

        public static TreeNode GetTypeNode()
        {
            MediaNode nodeType = new MediaNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
