using System;
using LiteDB;

namespace KnxUiEditorKeyTool
{
    public class LicenseEntry
    {
        private long _licenseId = DateTime.Now.Ticks;

        // ID
        [BsonId]
        public long LicenseId
        {
            get { return _licenseId; }
            set { _licenseId = value; }
        }
        // 企业名称
        public string EnterpriseName { get; set; }
        // 产品注册码
        public string SerialNumber { get; set; }
        // 订单日期
        public DateTime OrderTime { get; set; }
        // 到期时间
        public DateTime ExpireTime { get; set; }
        // 生成日期
        public DateTime CreateTime { get; set; }

    }
}