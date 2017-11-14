using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using UIEditor.Component;
using System.Threading;
using System.ComponentModel;
using GroupAddress;
using KNX.DatapointType;
using Utils;


namespace UIEditor.Component
{
    public static class ETSImport
    {
        private static string attrGroupAddress = "GroupAddress";
        private static string attrName = "Name";
        private static string attrAddress = "Address";
        private static string attrId = "Id";
        private static string attrDatapointType = "DatapointType";

        private static string strGroupAddressRefId = "GroupAddressRefId";
        private static string strSend = "Send";
        private static string strRefId = "RefId";

        private static string strComObject = "ComObject";
        private static string strComObjectRef = "ComObjectRef";

        private static string attrObjectSize = "ObjectSize";
        private static string attrRefId = "RefId";
        private static string attrText = "Text";
        private static string attrPriority = "Priority";
        private static string attrReadFlag = "ReadFlag";
        private static string attrWriteFlag = "WriteFlag";
        private static string attrCommunicationFlag = "CommunicationFlag";
        private static string attrTransmitFlag = "TransmitFlag";
        private static string attrUpdateFlag = "UpdateFlag";

        #region 解析ETS

        /// <summary>
        /// 解析 ETS 导出的地址文件
        /// </summary>
        /// <param name="groupAddressXml"></param>
        /// <returns></returns>
        public static List<ImGroupAddr> ParseGroupAddressXml(string groupAddressXml)
        {
            List<ImGroupAddr> knxGroupAddress = null;
            XNamespace xns = @"http://knx.org/xml/ga-export/01";

            if (File.Exists(groupAddressXml) == true)
            {
                XDocument addressXDoc = XDocument.Load(groupAddressXml);

                // 获取group address
                if (addressXDoc.Root != null)
                {
                    var groupAddress = from item in addressXDoc.Root.Descendants(xns + attrGroupAddress)
                                       select new ImGroupAddr
                                       {
                                           Id = Guid.NewGuid().ToString(),
                                           Name = item.Attribute(attrName).Value,
                                           KnxAddress = item.Attribute(attrAddress).Value,
                                           DPTName = (null != item.Attribute(attrDatapointType)) ? item.Attribute(attrDatapointType).Value : "",
                                           //Type = KNXDataType.Bit1,
                                       };

                    knxGroupAddress = groupAddress.ToList();

                    //parseDatapointTypeInGroupAddressList(knxGroupAddress);
                }
            }

            return knxGroupAddress;
        }

        /// <summary>
        /// 解析ETS文件
        /// </summary>
        /// <param name="etsProjectFile"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static List<ImGroupAddr> ParseEtsProjectFile(string etsProjectFile, BackgroundWorker worker)
        {
            List<ImGroupAddr> listAddress = new List<ImGroupAddr>();

            // 如果文件存在
            if (File.Exists(etsProjectFile) == true)
            {
                worker.ReportProgress(0, UIResMang.GetString("TextIsUnziping"));

                // 在当前位置解压文件
                string directoryName = Path.GetDirectoryName(etsProjectFile);
                ZipHelper.UnZipDir(etsProjectFile, directoryName);

                worker.ReportProgress(0, UIResMang.GetString("TextIsCaluculating"));

                // 查找 0.xml 文件
                const string addressFileName = "0.xml";
                var addressFiles = Directory.GetFiles(directoryName, addressFileName, SearchOption.AllDirectories);

                if (addressFiles.Length > 0)
                {
                    string addressFile = addressFiles[0];
                    var addressXDoc = XDocument.Load(addressFile);
                    addressXDoc.Element("KNX");
                    XNamespace xns = addressXDoc.Root.Name.Namespace;

                    // 从导入的ETS项目中获取group address
                    var groupAddress = from item in addressXDoc.Root.Descendants(xns + attrGroupAddress)
                                       select new ImGroupAddr
                                       {
                                           Id = item.Attribute(attrId).Value,
                                           Name = item.Attribute(attrName).Value,
                                           KnxAddress = item.Attribute(attrAddress).Value,
                                           DPTName = (null != item.Attribute(attrDatapointType)) ? item.Attribute(attrDatapointType).Value : "",
                                       };

                    // 获取数据类型
                    var comObjectInstanceRef = (from item in addressXDoc.Descendants(xns+strSend)
                                                let xElement = item.Parent
                                                where xElement != null
                                                select new
                                                {
                                                    GroupAddressRefId = item.Attribute(strGroupAddressRefId).Value,
                                                    ComObjectInstance = xElement.Parent,
                                                    //ComObjectInstanceRefId = xElement.Parent.Attribute(strRefId).Value,
                                                }).ToLookup(
                                                    p => p.GroupAddressRefId, 
                                                    p => p.ComObjectInstance
                                                    //p => p.ComObjectInstanceRefId
                                                    );

                    Dictionary<string, XDocument> xDocs = new Dictionary<string, XDocument>();

                    int len = groupAddress.ToList().Count;
                    float i = 0;

                    /* 获取组地址的大小 */
                    foreach (var address in groupAddress.ToList())
                    {
                        ImGroupAddr addr = new ImGroupAddr();
                        addr.Id = address.Id;
                        addr.Name = address.Name;
                        addr.KnxAddress =  KNXAddressHelper.AddressToString(Convert.ToUInt16(address.KnxAddress));
                        addr.DPTName = address.DPTName;
                        parseDatapointType(addr);

                        float f = i++ / len;
                        worker.ReportProgress((int)(f * 100), UIResMang.GetString("TextIsImportingGroupAddress") + " " + address.Name + " " + addr.KnxAddress);

                        if (comObjectInstanceRef.Contains(address.Id))
                        {
                            XElement comObjectInstance = comObjectInstanceRef[address.Id].First<XElement>();
                            if((null == addr.DPTName) || (addr.DPTName.Trim().Length <= 0) || (addr.DPTName.Contains("*"))) 
                            {
                                if (null != comObjectInstance.Attribute(attrDatapointType))
                                {
                                    addr.DPTName = comObjectInstance.Attribute(attrDatapointType).Value;
                                    parseDatapointType(addr);
                                }
                            }
                            var comObjectInstanceRefId = comObjectInstance.Attribute(strRefId).Value;

                            var index1 = comObjectInstanceRefId.IndexOf('_');
                            var index2 = comObjectInstanceRefId.IndexOf('_', index1 + 1);
                            var index3 = comObjectInstanceRefId.IndexOf('_', index2 + 1);
                            var comObjectInstanceFile = comObjectInstanceRefId.Substring(0, index2);
                            var comObjectInstanceId = comObjectInstanceRefId.Substring(0, index3);

                            var manufacturerDataFiles = Directory.GetFiles(directoryName, comObjectInstanceFile + ".xml", SearchOption.AllDirectories);
                            if (manufacturerDataFiles.Length > 0)
                            {
                                string manufacturerDataFile = manufacturerDataFiles[0];
                                XDocument manufacturerDataXDoc;
                                if (xDocs.ContainsKey(manufacturerDataFile))
                                {
                                    manufacturerDataXDoc = xDocs[manufacturerDataFile];
                                }
                                else
                                {
                                    manufacturerDataXDoc = XDocument.Load(manufacturerDataFile);
                                    xDocs.Add(manufacturerDataFile, manufacturerDataXDoc);
                                }

                                var comObject = (from p in manufacturerDataXDoc.Descendants(xns+strComObject)
                                                 where p.Attribute(attrId).Value == comObjectInstanceId
                                                 select p).FirstOrDefault();
                                parseComObject(addr, comObject);

                                var comObjectRef = (from p in manufacturerDataXDoc.Descendants(xns+strComObjectRef)
                                                    where p.Attribute(attrId).Value == comObjectInstanceRefId
                                                    select p).FirstOrDefault();
                                parseComObject(addr, comObjectRef);
                            }

                            //parseDatapointType(addr);
                            var formName = typeof(FrmGroupAddressMgt).Name;
                            if (Application.OpenForms[formName] != null)
                            {
                                var frm = Application.OpenForms[formName] as FrmGroupAddressMgt;
                                if (frm != null)
                                {
                                    if (frm.AddressIsExsit(addr.KnxAddress))
                                    {
                                        addr.IsSelected = false;
                                    }
                                }
                            }
                            //if (ProjResManager.AddressIsExsit(addr.KnxAddress))
                            //{
                            //    addr.IsSelected = false;
                            //}
                            listAddress.Add(addr);
                        }
                        else
                        {
                            //parseDatapointType(addr);
                            //addr.IsSelected = false;
                        }

                        //if (MyCache.AddressIsExsit(addr.KnxAddress))
                        //{
                        //    addr.IsSelected = false;
                        //}

                        //listAddress.Add(addr);
                    }
                }
            }

            return listAddress;
        }

        public static void parseComObject(ImGroupAddr address, XElement comObject)
        {
            var refId = comObject.Attribute(attrRefId);
            var text = comObject.Attribute(attrText);
            var priority = comObject.Attribute(attrPriority);
            var objectSize = comObject.Attribute(attrObjectSize);
            var readFlag = comObject.Attribute(attrReadFlag);
            var writeFlag = comObject.Attribute(attrWriteFlag);
            var communicationFlag = comObject.Attribute(attrCommunicationFlag);
            var transmitFlag = comObject.Attribute(attrTransmitFlag);
            var updateFlag = comObject.Attribute(attrUpdateFlag);
            var datapointType = comObject.Attribute(attrDatapointType);

            //
            //if (null != text)
            //{
                //address.Name = text.Value;
            //}

            if (null != priority)
            {
                address.Priority = KNXAddressHelper.GetKNXPriority(priority.Value);
            }

            if (null != datapointType && null != datapointType.Value)
            {
                if((null == address.DPTName) || (address.DPTName.Trim().Length <= 0) || (address.DPTName.Contains("*"))) 
                {
                    address.DPTName = datapointType.Value;
                    parseDatapointType(address);
                }
            }

            if (objectSize != null)
            {
                if((null == address.DPTName) || (address.DPTName.Trim().Length <= 0) || (address.DPTName.Contains("*"))) 
                {
                    // 数据类型
                    address.DPTName = DPTHelper.GetDPTMainName(KNXAddressHelper.GetKnxDataType(objectSize.Value));
                    if (address.DPTName.Length > 0)
                    {
                        address.IsSelected = true;
                        address.DPTNameIsDetermined = false;
                    }
                }
            }

            if (null != readFlag && null != readFlag.Value)
            {
                if ("Disabled" == readFlag.Value)
                {
                    address.IsRead = false;
                }
                else if ("Enabled" == readFlag.Value)
                {
                    address.IsRead = true;
                }
            }

            if (null != writeFlag && null != writeFlag.Value)
            {
                if ("Disabled" == writeFlag.Value)
                {
                    address.IsWrite = false;
                }
                else if ("Enabled" == writeFlag.Value)
                {
                    address.IsWrite = true;
                }
            }

            if (null != communicationFlag && null != communicationFlag.Value)
            {
                if ("Disabled" == communicationFlag.Value)
                {
                    address.IsCommunication = false;
                }
                else if ("Enabled" == communicationFlag.Value)
                {
                    address.IsCommunication = true;
                }
            }

            if (null != transmitFlag && null != transmitFlag.Value)
            {
                if ("Disabled" == transmitFlag.Value)
                {
                    address.IsTransmit = false;
                }
                else if ("Enabled" == transmitFlag.Value)
                {
                    address.IsTransmit = true;
                }
            }

            if (null != updateFlag && null != updateFlag.Value)
            {
                if ("Disabled" == updateFlag.Value)
                {
                    address.IsUpgrade = false;
                }
                else if ("Enabled" == updateFlag.Value)
                {
                    address.IsUpgrade = true;
                }
            }
        }

        /// <summary>
        /// 解析组地址列表中各组地址的数据点类型。主号、次号、大小、类型名
        /// </summary>
        /// <param name="addressList"></param>
        public static void parseDatapointType(ImGroupAddr address)
        {
            if (null == address)
            {
                return;
            }

            string dpst = address.DPTName;
            if ((null != dpst) && ((dpst.Length) > 0) && (dpst.Substring(0, 4).Equals("DPST")))
            {
                var index1 = dpst.IndexOf("-"); // 第一个"-"的位置
                var index2 = dpst.IndexOf("-", index1 + 1); // 第二个"-"的位置
                var index3 = dpst.IndexOf(" ", index2 + 1); // 空格的位置

                int mainNum = -1;
                int subNum = -1;
                if (index1 >= 4)
                {
                    if (index2 > (index1 + 1))
                    {
                        mainNum = Convert.ToInt32(dpst.Substring(index1 + 1, index2 - index1 - 1)); // 获取主号
                        if (index3 > (index2 + 1)) // 表示有多个数据类型。只取第一个
                        {
                            subNum = Convert.ToInt32(dpst.Substring(index2 + 1, index3 - index2)); // 整型子号
                        }
                        else
                        {
                            subNum = Convert.ToInt32(dpst.Substring(index2 + 1, dpst.Length - index2 - 1)); // 整型子号
                        }
                    }
                    else
                    {
                        mainNum = Convert.ToInt32(dpst.Substring(index1 + 1, dpst.Length - index1 - 1)); // 获取主号
                    }
                }

                if (MyCache.DicMainNumber.ContainsKey(mainNum))
                {
                    string knxMainNum = MyCache.DicMainNumber[mainNum];
                    string knxSubNum = DatapointType.DPST_ANY;
                    if (MyCache.DicSubNumber.ContainsKey(subNum))
                    {
                        knxSubNum = MyCache.DicSubNumber[subNum];
                    }

                    address.DPTName = DPTHelper.GetDPTName(knxMainNum, knxSubNum);
                    if (address.DPTName.Length > 0)
                    {
                        address.IsSelected = true;
                        address.DPTNameIsDetermined = true;
                    }
                }
            }
        }

        public static List<ImGroupAddr> ParseOpcFile(string opcFile, BackgroundWorker worker)
        {
            List<ImGroupAddr> listAddr = new List<ImGroupAddr>();

            string[] lines = File.ReadAllLines(opcFile, System.Text.Encoding.Default);
            foreach (string line in lines)
            {
                string[] elements = line.Split('\t');
                if (elements.Length >= 4)
                {
                    ImGroupAddr addr = new ImGroupAddr();
                    addr.Id = Guid.NewGuid().ToString();
                    addr.IsCommunication = true;
                    addr.IsRead = true;
                    addr.IsTransmit = true;
                    addr.IsUpgrade = true;
                    addr.IsWrite = true;

                    string[] group = elements[0].Split('.');
                    if (group.Length >= 3)
                    {
                        addr.KnxAddress = group[2];
                    }

                    addr.Name = elements[1];

                    worker.ReportProgress(0, addr.Name);
                    
                    int start = elements[2].IndexOf('(');
                    int end = elements[2].IndexOf(')');
                    if ((start > 0) && (end > start)) {
                        string type = elements[2].Substring(start+1, end-(start+1));
                        addr.DPTName = DPTHelper.GetDPTMainName(KNXAddressHelper.GetKnxDataType(type));
                        if (addr.DPTName.Length > 0)
                        {
                            //addr.IsSelected = true;
                            addr.DPTNameIsDetermined = false;
                        }
                    }

                    var formName = typeof(FrmGroupAddressMgt).Name;
                    if (Application.OpenForms[formName] != null)
                    {
                        var frm = Application.OpenForms[formName] as FrmGroupAddressMgt;
                        if (frm != null)
                        {
                            if (frm.AddressIsExsit(addr.KnxAddress))
                            {
                                addr.IsSelected = false;
                            }
                            else
                            {
                                addr.IsSelected = true;
                            }
                        }
                    }

                    addr.Priority = KNXAddressHelper.GetKNXPriority(elements[3]);

                    listAddr.Add(addr);
                }
            }

            return listAddr;
        }
        #endregion
    }
}
