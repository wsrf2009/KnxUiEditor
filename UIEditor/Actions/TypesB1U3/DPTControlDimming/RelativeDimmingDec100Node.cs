using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingDec100Node : DPTControlDimmingNode
    {
        public RelativeDimmingDec100Node() {
            action = new KNXDatapointAction();
            action.Name = Text = "调暗100%";
            action.Value = 0x01;
        }
    }
}
