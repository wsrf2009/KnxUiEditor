using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointAction
{
    public class DatapointActionNode : TreeNode
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public DatapointActionNode()
        {
            this.Name = "";
            this.Value = 0;
        }

        public DatapointActionNode(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public DatapointActionNode(KNXDatapointAction knx)
        {
            this.Name = knx.Name;
            this.Value = knx.Value;
        }

        public KNXDatapointAction ToKnx()
        {
            KNXDatapointAction knx = new KNXDatapointAction();
            knx.Name = this.Name;
            knx.Value = this.Value;

            return knx;
        }
    }
}
