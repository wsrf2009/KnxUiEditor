using KNX;
using KNX.DatapointAction;
using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIEditor.GroupAddress;

namespace GroupAddress
{
    public class MgGroupAddress : EdGroupAddress
    {
        #region 属性
        /// <summary>
        /// 是否选中该地址
        /// </summary>
        public bool IsSelected { get; set; }

        ///// <summary>
        ///// ETS中设备的ID， ETS自动分配
        ///// </summary>
        //public string Id { get; set; }

        ///// <summary>
        ///// ETS中设备用户指定的名称
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// ETS 写入地址
        ///// </summary>
        //public string KnxAddress { get; set; }

        ///// <summary>
        ///// ETS数据点名称
        ///// </summary>
        //public string DPTName { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsCommunication { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsRead { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsWrite { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsTransmit { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsUpgrade { get; set; }

        ///// <summary>
        ///// 优先级
        ///// </summary>
        //public KNXPriority Priority { get; set; }

        ////public string Actions { get; set; }
        //public GroupAddressActions Actions { get; set; }
        #endregion

        public MgGroupAddress()
        {

        }

        public MgGroupAddress(EdGroupAddress address)
            : base(address)
        {
            this.IsSelected = false;
            //this.Id = address.Id;
            //this.Name = address.Name;
            //this.KnxAddress = address.KnxAddress;
            //this.DPTName = address.DPTName;
            //this.IsCommunication = address.IsCommunication;
            //this.IsRead = address.IsRead;
            //this.IsWrite = address.IsWrite;
            //this.IsTransmit = address.IsTransmit;
            //this.IsUpgrade = address.IsUpgrade;
            //this.Priority = address.Priority;
            //this.Actions = address.Actions;
            //this.Actions = "";
            //if (address.Actions != null)
            //{
            //    foreach (DatapointActionNode action in address.Actions)
            //    {
            //        if (this.Actions.Length > 0)
            //        {
            //            this.Actions += "/" + action.ActionName;
            //        }
            //        else
            //        {
            //            this.Actions = action.ToString();
            //        }
            //    }
            //}
        }

        //public EdGroupAddress ToEdGroupAddress()
        //{
        //    var addr = new EdGroupAddress();

        //    addr.Id = this.Id;
        //    addr.Name = this.Name;
        //    addr.KnxAddress = this.KnxAddress;
        //    addr.Type = KNXDataType.Bit1;
        //    addr.KnxMainNumber = DatapointType.DPT_1;
        //    addr.KnxSubNumber = DatapointType.DPST_1;
        //    addr.DPTName = this.DPTName;
        //    addr.DefaultValue = "0";
        //    addr.Priority = KNXPriority.Low;
        //    addr.IsCommunication = true;
        //    addr.IsWrite = true;
        //    addr.IsRead = true;
        //    addr.IsTransmit = true;
        //    addr.IsUpgrade = true;
        //    addr.Actions = new GroupAddressActions();

        //    return addr
        //}

        public static MgGroupAddress GetGroupAddress(List<MgGroupAddress> list, string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return null;
            }

            foreach (var address in list)
            {
                if (address.Id == Id)
                {
                    return address;
                }
            }

            return null;
        }
    }
}
