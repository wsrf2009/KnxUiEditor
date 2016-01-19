using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Types8BitUnsignedValue.DPTScaling;

namespace UIEditor.Actions.Types8BitUnsignedValue.DPTScaling
{
    public class AbsoluteAdjustment90Node : ScalingNode
    {
        public AbsoluteAdjustment90Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调节至90%";
            action.Value = 229;
        }
    }
}
