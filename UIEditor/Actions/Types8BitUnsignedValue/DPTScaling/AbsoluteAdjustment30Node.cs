using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Types8BitUnsignedValue.DPTScaling;

namespace UIEditor.Actions.Types8BitUnsignedValue.DPTScaling
{
    public class AbsoluteAdjustment30Node : ScalingNode
    {
        public AbsoluteAdjustment30Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调节至30%";
            action.Value = 76;
        }
    }
}
