using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingInc100Node : DPTControlDimmingNode
    {
        public RelativeDimmingInc100Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调亮100%";
            action.Value = 0x09;
        }
    }
}
