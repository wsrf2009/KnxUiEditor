using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Reflection;  
using System.IO;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

namespace Upgrade
{
    /// <summary>  
    /// 更新完成触发的事件  
    /// </summary>  
    public delegate void DownloadProgressChangedDelegate(object sender, DownloadProgressChangedEventArgs e);
    public delegate void DownloadFileCompletedDelegate(object sender, AsyncCompletedEventArgs e);

    /// <summary>  
    /// 程序更新  
    /// </summary>  
    public partial class CheckUpdate
    {
        #region 变量
        private string DownloadUrl { get; set; }
        private string FileName { get; set; }
        #endregion

        #region 常量
        private const string updateUrl = "http://192.168.1.114/SVE/update.php";
        #endregion

        #region
        /// <summary>  
        /// 更新完成时触发的事件  
        /// </summary>  
        public event DownloadProgressChangedDelegate DownloadProgressChanged;
        public event DownloadFileCompletedDelegate DownloadFileCompleted;
        #endregion 
  
        /// <summary>  
        /// 下载更新  
        /// </summary>  
        public void Download(string dir, object obj)  
        {  
            try  
            {
                //string dir = obj as string;
                if (string.IsNullOrWhiteSpace(dir) || 
                    string.IsNullOrWhiteSpace(DownloadUrl) ||
                    string.IsNullOrWhiteSpace(FileName))
                {
                    return;
                }

                WebClient wc = new WebClient();
                string file = Path.Combine(dir, FileName);
                wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                wc.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                wc.DownloadFileAsync(new Uri(DownloadUrl), file, obj);

                wc.Dispose();  
            }  
            catch  
            {  
                throw new Exception("更新出现错误，网络连接失败！");  
            }  
        }  
  
        /// <summary>  
        /// 检查是否需要更新  
        /// </summary>  
        public UpdateInfo Check(string productName, string guid, string version)   
        {  
            //try
            //{
                string postString = "ProductName=" + productName + "&GUID=" + guid + "&Version="+version;//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
                byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                byte[] responseData = webClient.UploadData(updateUrl, "POST", postData);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码   

                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var info = JsonConvert.DeserializeObject<UpdateInfo>(srcString, settings);
                this.FileName = info.Name;
                this.DownloadUrl = info.Url;

                return info;
            //}  
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //return null;
        }

        public void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (null != DownloadProgressChanged)
            {
                DownloadProgressChanged(sender, e);
            }
        }

        public void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (null != DownloadFileCompleted)
            {
                DownloadFileCompleted(sender, e);
            }
        }
    }  
}