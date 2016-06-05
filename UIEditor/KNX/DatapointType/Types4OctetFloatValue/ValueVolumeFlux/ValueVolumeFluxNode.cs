using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueVolumeFlux
{
    class ValueVolumeFluxNode:Types4OctetFloatValueNode
    {
        public ValueVolumeFluxNode()
        {
            this.KNXSubNumber = DPST_77;
            this.Name = "volume flux (m³/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueVolumeFluxNode nodeType = new ValueVolumeFluxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
