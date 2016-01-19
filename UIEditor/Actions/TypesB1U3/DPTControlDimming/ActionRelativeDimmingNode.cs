using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class ActionRelativeDimmingNode : DPTControlDimmingNode
    {
        public ActionRelativeDimmingNode()
        {
            Text = "相对调光";

            this.Nodes.Add(new RelativeDimmingInc25Node());
            this.Nodes.Add(new RelativeDimmingInc50Node());
            this.Nodes.Add(new RelativeDimmingInc100Node());
            this.Nodes.Add(new RelativeDimmingDec25Node());
            this.Nodes.Add(new RelativeDimmingDec50Node());
            this.Nodes.Add(new RelativeDimmingDec100Node());
        }
    }
}
