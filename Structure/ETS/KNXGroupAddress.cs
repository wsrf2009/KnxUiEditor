
using System;
using System.Collections.Generic;


namespace Structure.ETS
{
    /// <summary>
    /// ETS项目中导入的数据。
    /// </summary>
    public class KNXGroupAddress
    {
        public KNXGroupAddress()
        {
            Id = Guid.NewGuid().ToString();
            Name = "New group address";
            KnxAddress = 1;
            Type = (int)KNXDataType.Bit1;
            KnxSize = "";
            KnxDPTName = "";
            Priority = (int)KNXPriority.Low;
            DefaultValue = "";
            IsCommunication = false;
            IsRead = false;
            IsWrite = false;
            IsTransmit = false;
            IsUpgrade = false;
            WireNumber = "";
            ReadTimeSpan = 0;
            Tip = "";
            Actions = null;
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
        public int KnxMainNumber { get; set; }

        /// <summary>
        /// ETS数据点子号
        /// </summary>
        public int KnxSubNumber { get; set; }

        /// <summary>
        /// ETS数据长度
        /// </summary>
        public string KnxSize { get; set; }

        /// <summary>
        /// ETS 数据类型
        /// </summary>
        public string KnxDPTName { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 优先级 
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

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
        /// 电缆编号
        /// </summary>
        public string WireNumber { get; set; }

        /// <summary>
        /// 读取时间间隔
        /// </summary>
        public int ReadTimeSpan { get; set; }

        /// <summary>
        /// 给最终用户控制该组地址时提供一些提示
        /// </summary>
        public string Tip { get; set; }

        /// <summary>
        /// ETS中该设备可执行的动作
        /// </summary>
        public List<KNXDatapointAction> Actions { get; set; }
    }
}
