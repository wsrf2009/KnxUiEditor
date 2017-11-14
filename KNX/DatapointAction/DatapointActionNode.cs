using System.Windows.Forms;

namespace KNX.DatapointAction
{
    public class DatapointActionNode : TreeNode
    {
        public string ActionName { get; set; }

        public int Value { get; set; }

        public DatapointActionNode()
        {
            this.ActionName = "";
            this.Value = 0;
        }

        public DatapointActionNode(string name, int value)
        {
            this.ActionName = name;
            this.Value = value;
        }

        public DatapointActionNode(DatapointAction knx)
        {
            this.ActionName = knx.Name;
            this.Value = knx.Value;
        }

        public DatapointAction ToKnx()
        {
            DatapointAction knx = new DatapointAction();
            knx.Name = this.ActionName;
            knx.Value = this.Value;

            return knx;
        }

        public override string ToString()
        {
            return this.ActionName;
        }
    }
}
