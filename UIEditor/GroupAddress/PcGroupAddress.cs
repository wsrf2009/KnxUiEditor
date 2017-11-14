using KNX.DatapointAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIEditor.GroupAddress;

namespace GroupAddress
{
    public class PcGroupAddress
    {
        #region 属性
        public bool IsSelected { get; set; }

        //public string Id { get; set; }

        //public string Name { get; set; }

        //public string KnxAddress { get; set; }

        //public string Type { get; set; }

        //public string DPTName { get; set; }

        //public string DefaultValue { get; set; }

        ////public int ReadTimeSpan { get; set; }

        ////public string Actions { get; set; }
        //public GroupAddressActions Actions { get; set; }
        #endregion

        //public PcGroupAddress(EdGroupAddress row)
        //{
        //    this.IsSelected = false;
        //    this.Id = row.Id;
        //    this.Name = row.Name;
        //    this.KnxAddress = row.KnxAddress;
        //    this.Type = row.Type;
        //    this.DPTName = row.DPTName;
        //    this.DefaultValue = row.DefaultValue;
        //    this.Actions = row.Actions;
        //    //this.Actions = "";
        //    //if (row.Actions != null)
        //    //{
        //    //    foreach (DatapointActionNode action in row.Actions)
        //    //    {
        //    //        if (this.Actions.Length > 0)
        //    //        {
        //    //            this.Actions += "/" + action.ActionName;
        //    //        }
        //    //        else
        //    //        {
        //    //            this.Actions = action.ActionName;
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
