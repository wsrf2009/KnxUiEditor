
using System;
using Structure;
using Structure.ETS;
using System.Collections.Generic;
using UIEditor.Component;
using UIEditor.KNX.DatapointType;
using UIEditor.KNX.DatapointAction;


namespace UIEditor.GroupAddress
{
    /// <summary>
    /// ETS项目中导入的数据。
    /// </summary>
    public class EdGroupAddress : IComparable, IEquatable<EdGroupAddress>
    {
        private static int _index;

        #region 属性
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
        public KNXDataType Type { get; set; }

        /// <summary>
        /// ETS数据点名称.1.*、1.1、1.2
        /// </summary>
        public string DPTName { get; set; }

        /// <summary>
        /// 数据点标签. switch、boolean、enable
        /// </summary>
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
        public KNXPriority Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 读取时间间隔，单位毫秒
        /// </summary>
        public int ReadTimeSpan { get; set; }

        ///// <summary>
        ///// 给最终用户控制该组地址时提供一些提示
        ///// </summary>
        //public string Tip { get; set; }

        /// <summary>
        /// ETS中该设备可执行的动作
        /// </summary>
        public List<DatapointActionNode> Actions { get; set; }
        //public string actions { get; set; }
        #endregion

        public EdGroupAddress()
        {
            _index++;

            this.Id = Guid.NewGuid().ToString();
            this.Name = ResourceMng.GetString("NewGroupAddress") + _index;
            this.KnxAddress = "1/1/1";
            this.Type = KNXDataType.Bit1;
            this.KnxMainNumber = DatapointType.DPT_1;
            this.KnxSubNumber = DatapointType.DPST_1;
            this.DPTName = MyCache.NodeTypes[0].Nodes[0].Text;
            //this.DPTText = "";
            this.DefaultValue = "0";
            this.Priority = KNXPriority.Low;
            this.IsCommunication = true;
            this.IsWrite = true;
            this.IsRead = true;
            this.IsTransmit = true;
            this.IsUpgrade = true;
            //this.Tip = null;
            this.Actions = new List<DatapointActionNode>(); ;
        }

        public EdGroupAddress(KNXGroupAddress address)
        {
            this.Id = address.Id;
            this.Name = address.Name;
            this.KnxAddress = KNXAddressHelper.AddressToString(address.KnxAddress);
            this.Type = (KNXDataType)address.Type;
            //this.KnxDPTName = address.KnxDPTName;
            //this.KnxSize = address.KnxSize;
            this.KnxMainNumber = address.KnxMainNumber;
            this.KnxSubNumber = address.KnxSubNumber;
            this.DPTName = address.DPTName;
            //this.DPTText = address.DPTText;
            this.DefaultValue = address.DefaultValue;
            this.Priority = (KNXPriority)address.Priority;
            //this.WireNumber = address.WireNumber;
            this.IsCommunication = address.IsCommunication;
            this.IsRead = address.IsRead;
            this.IsWrite = address.IsWrite;
            this.IsTransmit = address.IsTransmit;
            this.IsUpgrade = address.IsUpgrade;
            this.ReadTimeSpan = address.ReadTimeSpan;
            //this.Tip = address.Tip;
            this.Actions = new List<DatapointActionNode>();
            if (null != address.Actions)
            {
                foreach (KNXDatapointAction action in address.Actions)
                {
                    this.Actions.Add(new DatapointActionNode(action));
                }
            }
        }

        public KNXGroupAddress ToKnx()
        {
            var address = new KNXGroupAddress();

            address.Id = this.Id;
            address.Name = this.Name;
            address.KnxAddress = KNXAddressHelper.StringToAddress(this.KnxAddress);
            address.Type = (int)this.Type;
            address.KnxMainNumber = this.KnxMainNumber;
            address.KnxSubNumber = this.KnxSubNumber;
            //address.KnxSize = this.KnxSize;
            //address.KnxDPTName = this.KnxDPTName;
            //address.Type = (int)this.Type;
            address.DPTName = this.DPTName;
            //address.DPTText = this.DPTText;
            address.DefaultValue = this.DefaultValue;
            address.Priority = (int)this.Priority;
            //address.WireNumber = this.WireNumber;
            address.IsCommunication = this.IsCommunication;
            address.IsRead = this.IsRead;
            address.IsWrite = this.IsWrite;
            address.IsTransmit = this.IsTransmit;
            address.IsUpgrade = this.IsUpgrade;
            address.ReadTimeSpan = this.ReadTimeSpan;
            //address.Tip = this.Tip;
            address.Actions = new List<KNXDatapointAction>();
            if (null != address.Actions)
            {
                foreach (DatapointActionNode node in this.Actions)
                {
                    address.Actions.Add(node.ToKnx());
                }
            }

            return address;
        }



        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            EdGroupAddress address = obj as EdGroupAddress;
            if (address != null)
            {
                return Id.CompareTo(address.Id);
            }

            throw new ArgumentException("Object is not a KNXGroupAddress");
        }

        public bool Equals(EdGroupAddress other)
        {
            if (other == null) { return false; }

            if (this.Id == other.Id) { return true; }

            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }

            EdGroupAddress address = obj as EdGroupAddress;
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

        public static bool operator ==(EdGroupAddress addressA, EdGroupAddress addressB)
        {
            if ((object)addressA == null || ((object)addressB) == null)
            {
                return Object.Equals(addressA, addressB);
            }

            return addressA.Equals(addressB);
        }

        public static bool operator !=(EdGroupAddress addressA, EdGroupAddress addressB)
        {
            if (addressA == null || addressB == null)
            {
                return !Object.Equals(addressA, addressB);
            }

            return !(addressA.Equals(addressB));
        }

    }
}
