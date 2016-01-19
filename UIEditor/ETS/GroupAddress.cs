
using System;
using Structure;
using Structure.ETS;
using System.Collections.Generic;


namespace UIEditor.ETS
{
    /// <summary>
    /// ETS项目中导入的数据。
    /// </summary>
    public class GroupAddress : IComparable, IEquatable<GroupAddress>
    {
        private static int _index;
        public GroupAddress()
        {
            _index++;
            Id = Guid.NewGuid().ToString();
            Name = "新建组地址_" + _index;
            KnxAddress = "0/0/1";
            Type = KNXDataType.Bit1;
            KnxMainNumber = -1;
            KnxSubNumber = -1;
            KnxSize = "";
            KnxDPTName = "";
            DefaultValue = "0";
            Priority = KNXPriority.Low;
            IsCommunication = true;
            IsWrite = true;
            IsRead = false;
            IsTransmit = true;
            IsUpgrade = true;
            WireNumber = "";
            Tip = null;
            Actions = null;
        }

        public GroupAddress(KNXGroupAddress address)
        {
            this.Id = address.Id;
            this.Name = address.Name;
            this.KnxAddress = ETSImport.AddressToString(address.KnxAddress);
            this.Type = (KNXDataType)address.Type;
            this.KnxDPTName = address.KnxDPTName;
            this.KnxSize = address.KnxSize;
            this.KnxMainNumber = address.KnxMainNumber;
            this.KnxSubNumber = address.KnxSubNumber;
            this.DefaultValue = address.DefaultValue;
            this.Priority = (KNXPriority)address.Priority;
            this.WireNumber = address.WireNumber;
            this.IsCommunication = address.IsCommunication;
            this.IsRead = address.IsRead;
            this.IsWrite = address.IsWrite;
            this.IsTransmit = address.IsTransmit;
            this.IsUpgrade = address.IsUpgrade;
            this.ReadTimeSpan = address.ReadTimeSpan;
            this.Tip = address.Tip;
            this.Actions = address.Actions;
        }

        public KNXGroupAddress ToKnx()
        {
            var address = new KNXGroupAddress();

            address.Id = this.Id;
            address.Name = this.Name;
            address.KnxAddress = ETSImport.StringToAddress(this.KnxAddress);
            address.Type = (int)this.Type;
            address.KnxMainNumber = this.KnxMainNumber;
            address.KnxSubNumber = this.KnxSubNumber;
            address.KnxSize = this.KnxSize;
            address.KnxDPTName = this.KnxDPTName;
            address.Type = (int)this.Type;
            address.DefaultValue = DefaultValue;
            address.Priority = (int)Priority;
            address.WireNumber = this.WireNumber;
            address.IsCommunication = this.IsCommunication;
            address.IsRead = this.IsRead;
            address.IsWrite = this.IsWrite;
            address.IsTransmit = this.IsTransmit;
            address.IsUpgrade = this.IsUpgrade;
            address.ReadTimeSpan = this.ReadTimeSpan;
            address.Tip = this.Tip;
            address.Actions = this.Actions;

            return address;
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
        public string KnxAddress { get; set; }

        /// <summary>
        /// ETS数据点号
        /// </summary>
        public string KnxDatapointType { get; set; }

        /// <summary>
        /// ETS数据点主号
        /// </summary>
        public int KnxMainNumber { get; set; }

        /// <summary>
        /// ETS数据点子号
        /// </summary>
        public int KnxSubNumber { get; set; }

        /// <summary>
        /// ETS数据点大小
        /// </summary>
        public string KnxSize { get; set; }

        /// <summary>
        /// ETS数据点名称
        /// </summary>
        public string KnxDPTName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public KNXDataType Type { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public KNXPriority Priority { get; set; }

        /// <summary>
        /// 
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
        /// 读取时间间隔，单位毫秒
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
        //public string actions { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            GroupAddress address = obj as GroupAddress;
            if (address != null)
            {
                return Id.CompareTo(address.Id);
            }

            throw new ArgumentException("Object is not a KNXGroupAddress");
        }

        public bool Equals(GroupAddress other)
        {
            if (other == null) { return false; }

            if (this.Id == other.Id) { return true; }

            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }

            GroupAddress address = obj as GroupAddress;
            if (address == null)
            {
                return false;
            }

            return Equals(address);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(GroupAddress addressA, GroupAddress addressB)
        {
            if ((object)addressA == null || ((object)addressB) == null)
            {
                return Object.Equals(addressA, addressB);
            }

            return addressA.Equals(addressB);
        }

        public static bool operator !=(GroupAddress addressA, GroupAddress addressB)
        {
            if (addressA == null || addressB == null)
            {
                return !Object.Equals(addressA, addressB);
            }

            return !(addressA.Equals(addressB));
        }

    }
}
