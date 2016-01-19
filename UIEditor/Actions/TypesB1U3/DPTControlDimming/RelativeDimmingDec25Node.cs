using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingDec25Node : DPTControlDimmingNode
    {
        public RelativeDimmingDec25Node() {
            action = new KNXDatapointAction();
            action.Name = Text = "调暗25%";
            action.Value = 0x03;
        }
    }
}
