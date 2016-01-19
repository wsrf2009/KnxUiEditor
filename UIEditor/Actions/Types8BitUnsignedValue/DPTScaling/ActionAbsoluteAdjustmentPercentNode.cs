using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Types8BitUnsignedValue.DPTScaling;

namespace UIEditor.Actions.Types8BitUnsignedValue.DPTScaling
{
    public class ActionAbsoluteAdjustmentPercentNode : ScalingNode
    {
        public ActionAbsoluteAdjustmentPercentNode()
        {
            Text = "绝对调节（百分比）";

            this.Nodes.Add(new AbsoluteAdjustment30Node());
            this.Nodes.Add(new AbsoluteAdjustment60Node());
            this.Nodes.Add(new AbsoluteAdjustment90Node());
        }
    }
}
