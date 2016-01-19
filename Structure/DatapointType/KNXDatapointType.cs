using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure.ETS
{
    public class KNXDatapointType
    {
        public int MainNumber { get; set; }
        public string Format { get; set; }
        public string Size { get; set; }
        public List<KNXDatapointTypeSub> subs { get; set; }
    }

    public class KNXDatapointTypeSub
    {
        public int SubNumber { get; set; }
        public string DPTName { get; set; }

        //public List<KNXDatapointAction> actions { get; set; }
    }

    public class KNXDatapointAction
    {
        public KNXDatapointAction()
        {
        }

        public KNXDatapointAction(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public KNXDatapointAction(string name, int value, bool delete)
        {
            this.Name = name;
            this.Value = value;
            this.CanBeDelete = delete;
        }

        public string Name { get; set; }

        public string Encoding { get; set; }

        public int Value { get; set; }

        public bool CanBeDelete { get; set; }
    }
}
