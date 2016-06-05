using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.MasterSlaveMode
{
    class MasterSlaveModeNode:TypesN8Node
    {
        public MasterSlaveModeNode()
        {
            this.KNXSubNumber = DPST_112;
            this.Name = "master/slave mode";
        }

        public static TreeNode GetTypeNode()
        {
            MasterSlaveModeNode nodeType = new MasterSlaveModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
