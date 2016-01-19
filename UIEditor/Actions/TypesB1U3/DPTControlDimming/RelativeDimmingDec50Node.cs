using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingDec50Node : DPTControlDimmingNode
    {
        public RelativeDimmingDec50Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调暗50%";
            action.Value = 0x02;
        }
    }
}
