using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.GroupAddress
{
    public class PcGroupAddress
    {
        #region 属性
        public bool Selected { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string KnxAddress { get; set; }

        public string Type { get; set; }

        public string DPTName { get; set; }

        //public bool IsCommunication { get; set; }

        //public bool IsRead { get; set; }

        //public bool IsWrite { get; set; }

        public string DefaultValue { get; set; }

        public int ReadTimeSpan { get; set; }

        public string Actions { get; set; }
        #endregion

        public PcGroupAddress(EdGroupAddress row)
        {
            this.Selected = false;
            this.Id = row.Id;
            this.Name = row.Name;
            this.KnxAddress = row.KnxAddress;
            this.Type = row.Type.ToString();
            this.DPTName = row.DPTName;
            //this.IsCommunication = row.IsCommunication;
            //this.IsRead = row.IsRead;
            //this.IsWrite = row.IsWrite;
            this.DefaultValue = row.DefaultValue;
            this.ReadTimeSpan = row.ReadTimeSpan;
            this.Actions = "";
            if (row.Actions != null)
            {
                foreach (DatapointActionNode action in row.Actions)
                {
                    if (this.Actions.Length > 0)
                    {
                        this.Actions += "/" + action.Name;
                    }
                    else
                    {
                        this.Actions = action.Name;
                    }
                }
            }
        }


    }
}
