using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEditor.GroupAddress
{
    public class ImGroupAddr
    {
        #region
        /// <summary>
        /// ETS中设备的ID， ETS自动分配
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否选中该地址
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// ETS中设备用户指定的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ETS 写入地址
        /// </summary>
        public string KnxAddress { get; set; }

        /// <summary>
        /// ETS数据点名称.1.*、1.001、1.002
        /// </summary>
        public string DPTName { get; set; }

        public bool DPTNameIsDetermined { get; set; }

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
        #endregion

        public ImGroupAddr()
        {
            this.Id = "";
            this.IsSelected = false;
            this.Name = "";
            this.KnxAddress = "";
            this.DPTName = "";
            this.DPTNameIsDetermined = false;
            this.IsCommunication = false;
            this.IsRead = false;
            this.IsWrite = false;
            this.IsTransmit = false;
            this.IsUpgrade = false;
            this.Priority = KNXPriority.Low;
        }
    }
}
