using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSoundIntensity
{
    class ValueSoundIntensityNode:Types4OctetFloatValueNode
    {
        public ValueSoundIntensityNode()
        {
            this.KNXSubNumber = DPST_64;
            this.Name = "sound intensity (W/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueSoundIntensityNode nodeType = new ValueSoundIntensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
