
using System;
using System.Collections.Generic;
using UIEditor.KNX.DatapointAction;


namespace Structure.ETS
{
    /// <summary>
    /// ETS项目中导入的数据。
    /// </summary>
    public class KNXGroupAddress
    {
        public KNXGroupAddress()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "New group address";
            this.KnxAddress = 1;
            this.Type = (int)KNXDataType.Bit1;
            //KnxSize = "";
            //KnxDPTName = "";
            this.KnxMainNumber = "*";
            this.KnxSubNumber = "*";
            this.DPTName = "";
            //this.DPTText = "";
            this.Priority = (int)KNXPriority.Low;
            this.DefaultValue = "";
            this.IsCommunication = false;
            this.IsRead = false;
            this.IsWrite = false;
            this.IsTransmit = false;
            this.IsUpgrade = false;
            //WireNumber = "";
            this.ReadTimeSpan = 0;
            //this.Tip = "";
            this.Actions = null;
        }

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
        public ushort KnxAddress { get; set; }

        /// <summary>
        /// ETS数据点主号
        /// </summary>
        public string KnxMainNumber { get; set; }

        /// <summary>
        /// ETS数据点子号
        /// </summary>
        public string KnxSubNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// ETS数据点名称.1.*、1.001、1.002
        /// </summary>
        public string DPTName { get; set; }

        ///// <summary>
        ///// 数据点标签. switch、boolean、enable
        ///// </summary>
        //public string DPTText { get; set; }



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
        public int Priority { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        ///// <summary>
        ///// 电缆编号
        ///// </summary>
        //public string WireNumber { get; set; }

        /// <summary>
        /// 读取时间间隔
        /// </summary>
        public int ReadTimeSpan { get; set; }

        ///// <summary>
        ///// 给最终用户控制该组地址时提供一些提示
        ///// </summary>
        //public string Tip { get; set; }

        /// <summary>
        /// ETS中该设备可执行的动作
        /// </summary>
        public List<KNXDatapointAction> Actions { get; set; }
    }
}
