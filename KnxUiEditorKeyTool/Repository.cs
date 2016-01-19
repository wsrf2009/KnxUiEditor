using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using NLog;

namespace KnxUiEditorKeyTool
{
    /// <summary>
    /// 本地文件数据库
    /// </summary>
    public class Repository
    {
        //
        private static Logger Log = LogManager.GetCurrentClassLogger();

        // 数据库名称
        private readonly string _databaseName = "";

        // 集合
        private readonly string _tableName = "License";

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public Repository(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException("databaseName");
            }

            this._databaseName = databaseName;

            InitDb();
        }

        private string ConnectionString
        {
            get
            {
                return string.Format("filename={0};journal=true", _databaseName);
            }
        }

        // 数据库实例
        private LiteEngine _db;

        private void InitDb()
        {
            _db = new LiteEngine(this.ConnectionString);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entName"></param>
        /// <returns></returns>
        public IList<LicenseEntry> Get(string entName)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            var filtered = collection.Find(i => i.EnterpriseName.Equals(entName));
            return filtered.ToList();

        }

        public bool Contain(long licenseId)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            var filtered = collection.FindById(licenseId);

            if (filtered != null)
            {
                return true;
            }

            return false;
        }

        public LicenseEntry Get(long licenseId)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            return collection.FindById(licenseId);
        }

        #endregion

        #region 操作
        /// <summary>
        /// 写入文件数据
        /// </summary>
        /// <param name="licenseItem"></param>
        public void Add(LicenseEntry licenseItem)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            collection.Insert(licenseItem);
            UpdateIndex(collection);

        }

        /// <summary>
        /// 更新文件条目
        /// </summary>
        /// <param name="licenseItem"></param>
        public void Update(LicenseEntry licenseItem)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            collection.Update(licenseItem);
            UpdateIndex(collection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseItem"></param>
        public void Replace(LicenseEntry licenseItem)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            var filtered = collection.FindById(licenseItem.LicenseId);
            if (filtered == null)
            {
                collection.Insert(licenseItem);
            }
            else
            {
                collection.Update(licenseItem);
            }

            UpdateIndex(collection);
        }

        public void Delete(long licenseId)
        {
            var collection = _db.GetCollection<LicenseEntry>(_tableName);
            collection.Delete(x => x.LicenseId == licenseId);
            UpdateIndex(collection);
        }

        /// <summary>
        /// 更新索引
        /// </summary>
        private void UpdateIndex(Collection<LicenseEntry> collection)
        {
            // License ID
            collection.EnsureIndex(x => x.LicenseId);
            // 企业名称
            collection.EnsureIndex(x => x.EnterpriseName);
            // license key
            collection.EnsureIndex(x => x.SerialNumber);
        }

        #endregion
    }
}
