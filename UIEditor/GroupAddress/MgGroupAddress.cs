using Structure;
using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.GroupAddress
{
    public class MgGroupAddress
    {
        #region 属性
        /// <summary>
        /// 是否选中该地址
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// ETS中设备的ID， ETS自动分配
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ETS中设备用户指定的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ETS 写入地址
        /// </summary>
        public string KnxAddress { get; set; }

        /// <summary>
        /// ETS数据点名称
        /// </summary>
        public string DPTName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCommunication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsWrite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTransmit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUpgrade { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public KNXPriority Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 读取时间间隔，单位毫秒
        /// </summary>
        public int ReadTimeSpan { get; set; }

        public string Actions { get; set; }

        #endregion

        public MgGroupAddress(EdGroupAddress address)
        {
            this.IsSelected = false;
            this.Id = address.Id;
            this.Name = address.Name;
            this.KnxAddress = address.KnxAddress;
            this.DPTName = address.DPTName;
            this.IsCommunication = address.IsCommunication;
            this.IsRead = address.IsRead;
            this.IsWrite = address.IsWrite;
            this.IsTransmit = address.IsTransmit;
            this.IsUpgrade = address.IsUpgrade;
            this.Priority = address.Priority;
            this.DefaultValue = address.DefaultValue;
            this.ReadTimeSpan = address.ReadTimeSpan;
            this.Actions = "";
        }
    }
}
