using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingInc50Node : DPTControlDimmingNode
    {
        public RelativeDimmingInc50Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调亮50%";
            action.Value = 0x0A;
        }
    }
}
