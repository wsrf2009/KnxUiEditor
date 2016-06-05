using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetSignedValue.ReactiveEnergy
{
    class ReactiveEnergyNode:Types4OctetSignedValueNode
    {
        public ReactiveEnergyNode()
        {
            this.KNXSubNumber = DPST_12;
            this.Name = "reactive energy (VARh)";
        }

        public static TreeNode GetTypeNode()
        {
            ReactiveEnergyNode nodeType = new ReactiveEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
