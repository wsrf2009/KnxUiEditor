﻿using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypesB1U3.DPTControlDimming
{
    public class RelativeDimmingInc25Node : DPTControlDimmingNode
    {
        public RelativeDimmingInc25Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "调亮25%";
            action.Value = 0x0B;
        }
    }
}