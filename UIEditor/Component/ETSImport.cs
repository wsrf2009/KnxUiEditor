using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Structure;
using UIEditor.ETS;
using Structure.ETS;

namespace UIEditor
{
    public static class ETSImport
    {
        private static string attrGroupAddress = "GroupAddress";
        private static string attrName = "Name";
        private static string attrAddress = "Address";
        private static string attrId = "Id";
        private static string attrDatapointType = "DatapointType";

        private static string attrObjectSize = "ObjectSize";
        private static string attrComObject = "ComObject";

        //
        private const string ETS5 = "ETS5";
        private const string ETS4 = "ETS4";
        private const string ETS3 = "ETS3";

        #region 解析ETS

        /// <summary>
        /// 解析 ETS 导出的地址文件
        /// </summary>
        /// <param name="groupAddressXml"></param>
        /// <returns></returns>
        public static List<GroupAddress> ParseGroupAddressXml(string groupAddressXml)
        {
            List<GroupAddress> knxGroupAddress = null;
            XNamespace xns = @"http://knx.org/xml/ga-export/01";

            if (File.Exists(groupAddressXml) == true)
            {
                XDocument addressXDoc = XDocument.Load(groupAddressXml);

                // 获取group address
                if (addressXDoc.Root != null)
                {
                    var groupAddress = from item in addressXDoc.Root.Descendants(xns + attrGroupAddress)
                                       select new GroupAddress
                                       {
                                           Id = Guid.NewGuid().ToString(),
                                           Name = item.Attribute(attrName).Value,
                                           KnxAddress = item.Attribute(attrAddress).Value,
                                           KnxDatapointType = item.Attribute(attrDatapointType).Value,
                                           Type = KNXDataType.Bit1,
                                       };

                    knxGroupAddress = groupAddress.ToList();

                    parseDatapointTypeInGroupAddressList(knxGroupAddress);
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
        public static List<GroupAddress> ParseEtsProjectFile(string etsProjectFile)
        {
            List<GroupAddress> knxGroupAddress = null;

            // 如果文件存在
            if (File.Exists(etsProjectFile) == true)
            {
                // 在当前位置解压文件
                string directoryName = Path.GetDirectoryName(etsProjectFile);
                ZipHelper.UnZipDir(etsProjectFile, directoryName);

                //XNamespace xns = @"http://knx.org/xml/project/11";
                XNamespace xns = @"http://knx.org/xml/project/12";

                // 查找 0.xml 文件
                const string addressFileName = "0.xml";
                var addressFiles = Directory.GetFiles(directoryName, addressFileName, SearchOption.AllDirectories);

                if (addressFiles.Length > 0)
                {
                    string addressFile = addressFiles[0];
                    var addressXDoc = XDocument.Load(addressFile);
                    addressXDoc.Element("KNX");

                    // 从导入的ETS项目中获取group address
                    var groupAddress = from item in addressXDoc.Root.Descendants(xns + attrGroupAddress)
                                       select new GroupAddress
                                       {
                                           Id = item.Attribute(attrId).Value,
                                           Name = item.Attribute(attrName).Value,
                                           KnxAddress = item.Attribute(attrAddress).Value,
                                           KnxDatapointType = item.Attribute(attrDatapointType).Value,
                                           Type = KNXDataType.Bit1,

                                           //string datapointType = item.Attribute(attrDatapointType).Value,

                                           //var index1 = datapointType.IndexOf("-");
                                           //var index2 = datapointType.IndexOf("-", index1 + 1);
                                           //string strMainNumber = datapointType.Substring(index1 + 1, index2 - index1 - 1);
                                           // string strSubNumber = datapointType.Substring(index2 + 1, datapointType.Length - index2 - 1);
                                           // uint mainNumber = uint.Parse(strMainNumber);
                                           // uint subNumber = uint.Parse(strSubNumber);
                                       };

                    knxGroupAddress = groupAddress.ToList();

                    Debug.WriteLine(groupAddress);

                    /* 解析组地址的大小和类型 */
                    parseDatapointTypeInGroupAddressList(knxGroupAddress);

                    // 获取数据类型
                    string strGroupAddressRefId = "GroupAddressRefId";
                    string strSend = "Send";
                    string strRefId = "RefId";
                    string strComObjectRef = "ComObjectRef";
                    var comObjectInstanceRef = (from item in addressXDoc.Root.Descendants(xns + strSend)
                                                let xElement = item.Parent
                                                where xElement != null
                                                select new
                                                {
                                                    GroupAddressRefId = item.Attribute(strGroupAddressRefId).Value,
                                                    ComObjectInstanceRefId = xElement.Parent.Attribute(strRefId).Value,
                                                }).ToLookup(p => p.GroupAddressRefId, p => p.ComObjectInstanceRefId);

                    Debug.WriteLine(comObjectInstanceRef);

                    /* 获取组地址的大小 */
                    foreach (var address in knxGroupAddress)
                    {
                        if (comObjectInstanceRef.Contains(address.Id))
                        {
                            var comObjectInstanceRefId = comObjectInstanceRef[address.Id].First<string>();

                            var index1 = comObjectInstanceRefId.IndexOf('_');
                            var index2 = comObjectInstanceRefId.IndexOf('_', index1 + 1);
                            var comObjectInstanceFile = comObjectInstanceRefId.Substring(0, index2);

                            var manufacturerDataFiles = Directory.GetFiles(directoryName, comObjectInstanceFile + ".xml", SearchOption.AllDirectories);
                            if (manufacturerDataFiles.Length > 0)
                            {
                                string manufacturerDataFile = manufacturerDataFiles[0];
                                var manufacturerDataXDoc = XDocument.Load(manufacturerDataFile);

                                var objectSize = (from p in manufacturerDataXDoc.Root.Descendants(xns + strComObjectRef)
                                                  where p.Attribute(attrId).Value == comObjectInstanceRefId
                                                  select p.Attribute(attrObjectSize)).FirstOrDefault();

                                //
                                if (objectSize != null)
                                {
                                    // 数据类型
                                    address.Type = GetKnxDataType(objectSize.Value);
                                }
                                else
                                {
                                    var comObjectId = (from p in manufacturerDataXDoc.Root.Descendants(xns + strComObjectRef)
                                                       where p.Attribute(attrId).Value == comObjectInstanceRefId
                                                       select p.Attribute(strRefId).Value).FirstOrDefault();

                                    var objectSize2 = (from p in manufacturerDataXDoc.Root.Descendants(xns + attrComObject)
                                                       where p.Attribute(attrId).Value == comObjectId
                                                       select p.Attribute(attrObjectSize)).FirstOrDefault();

                                    if (objectSize2 != null)
                                    {
                                        // 数据类型
                                        address.Type = GetKnxDataType(objectSize2.Value);
                                    }
                                }

                                //if (KNXDataType.Bit1 == address.Type)
                                //{
                                //    address.Actions = DefaultActions.Bit1ActionsDefault();

                                //}

                                Debug.WriteLine(objectSize);
                            }
                        }
                    }
                }

                foreach (var it in knxGroupAddress)
                {
                    it.KnxAddress = AddressToString(Convert.ToUInt16(it.KnxAddress));
                }

                //var settings = new JsonSerializerSettings();
                //settings.TypeNameHandling = TypeNameHandling.Auto;
                //string json = JsonConvert.SerializeObject(knxGroupAddress, Formatting.Indented, settings);
                //File.WriteAllText(MyConst.GroupAddressFile, json);
            }

            return knxGroupAddress;
        }

        /// <summary>
        /// 解析组地址列表中各组地址的数据点类型。主号、次号、大小、类型名
        /// </summary>
        /// <param name="addressList"></param>
        public static void parseDatapointTypeInGroupAddressList(List<GroupAddress> addressList)
        {
            foreach (var address in addressList)
            {
                string dpst = address.KnxDatapointType;
                //Console.Write("\n" + "dpst:" + dpst);
                if ((dpst.Length) > 0 && (dpst.Substring(0, 4).Equals("DPST")))
                {
                    var index1 = dpst.IndexOf("-"); // 第一个"-"的位置
                    var index2 = dpst.IndexOf("-", index1 + 1); // 第二个"-"的位置
                    var index3 = dpst.IndexOf(" ", index2 + 1); // 空格的位置
                    //Console.Write(" index1:" + index1 + " index2:" + index2);
                    if ((index1 > 0) && (index2 > 0))
                    {
                        string strMainNumber = dpst.Substring(index1 + 1, index2 - index1 - 1);
                        string strSubNumber;
                        if (index3 > (index2 + 1)) // 表示有多个数据类型。只取第一个
                        {
                            strSubNumber = dpst.Substring(index2 + 1, index3 - index2);
                        }
                        else
                        {
                            strSubNumber = dpst.Substring(index2 + 1, dpst.Length - index2 - 1);
                        }

                        int mainNumber = int.Parse(strMainNumber);
                        int subNumber = int.Parse(strSubNumber);
                        address.KnxMainNumber = mainNumber;
                        address.KnxSubNumber = subNumber;

                        //Console.Write(" mainNumber:" + mainNumber + " subNumber:" + subNumber);

                        foreach (var datapointType in MyCache.DatapointTypeTable)
                        {
                            if (datapointType.MainNumber == mainNumber)
                            {
                                address.KnxSize = datapointType.Size;
                                address.Type = GetKnxDataType(address.KnxSize);
                                foreach (var sub in datapointType.subs)
                                {
                                    if (sub.SubNumber == subNumber)
                                    {
                                        address.KnxDPTName = sub.DPTName;
                                        //if (sub.actions != null)
                                        //{
                                        //    address.Actions = sub.actions;
                                        //}
                                        break;
                                    }
                                }
                                break;
                            }
                        }

                        //if (1 == mainNumber)
                        //{
                        //    if (1 == subNumber)
                        //    {
                        //        address.Actions = new List<KNXDatapointAction>();
                        //        address.Actions.Add(new KNXDatapointAction("开", 1, false));
                        //        address.Actions.Add(new KNXDatapointAction("关", 0, false));
                        //    }
                        //}
                        //else if (3 == mainNumber)
                        //{
                        //    if (7 == subNumber) 
                        //    {
                        //        address.Actions = new List<KNXDatapointAction>();
                        //        address.Actions.Add(new KNXDatapointAction("调亮 2%", 0x07, false)); // 1.5625%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 3%", 0x06, false)); // 3.125%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 6%", 0x05, false)); // 6.25%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 13%", 0x04, false)); // 12.5%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 25%", 0x03, false)); // 25%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 50%", 0x02, false)); // 50%
                        //        address.Actions.Add(new KNXDatapointAction("调亮 100%", 0x01, false)); // 100%

                        //        address.Actions.Add(new KNXDatapointAction("调暗 2%", 0x08 | 0x07, false)); // 1.5625%
                        //        address.Actions.Add(new KNXDatapointAction("调暗 3%", 0x08 | 0x06, false)); // 3.125 %
                        //        address.Actions.Add(new KNXDatapointAction("调暗 6%", 0x08 | 0x05, false)); // 6.25%
                        //        address.Actions.Add(new KNXDatapointAction("调暗 13%", 0x08 | 0x04, false)); // 12.5%
                        //        address.Actions.Add(new KNXDatapointAction("调暗 25%", 0x08 | 0x03, false)); // 25%
                        //        address.Actions.Add(new KNXDatapointAction("调暗 50%", 0x08 | 0x02, false)); // 50%
                        //        address.Actions.Add(new KNXDatapointAction("调暗 100%", 0x08 | 0x01, false)); // 100%
                        //    }
                        //}
                        //else if (18 == mainNumber)
                        //{
                        //    if (1 == subNumber) 
                        //    {
                        //        address.Actions = new List<KNXDatapointAction>();
                        //        for (int i = 1; i <= 64; i++)
                        //        {
                        //            address.Actions.Add(new KNXDatapointAction(string.Format("场景%d", i), i-1, false));
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// group 地址的分组表示
        /// group address 是一个 uint16 的整数， 
        /// 高 8 位被分成三部分， 第一位保留位， 第 2-5 位是主地址组，第 6-8 位是辅地址组，
        /// 低 8 位是地址 1～255
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string AddressToString(UInt16 address)
        {
            //高8位模
            UInt16 HMod = 0xff00;
            // 低8位模
            UInt16 LMod = 0x00ff;

            // 去高位和低位
            var highByte = (address & HMod) >> 8;
            var lowByte = address & LMod;

            // 对高8位进一步拆分，前5位为主分组，剩下3位是中间分组
            var mainAddress = (highByte >> 3) & 0x0f;
            var middleAddress = highByte & 0x07;

            return string.Format("{0}/{1}/{2}", mainAddress, middleAddress, lowByte);
        }

        public static UInt16 StringToAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("address");
            }

            var temp = address.Split('/');

            int result = 0;

            if (temp.Length == 3)
            {
                // Lbyte
                result = Convert.ToInt32(temp[2]);
                // middleAddress
                result = result + Convert.ToInt32(temp[1]) * 256;
                // mainAddress
                result = result + Convert.ToInt32(temp[0]) * 2048;
            }
            else
            {
                throw new ArithmeticException("address");
            }

            return Convert.ToUInt16(result);
        }

        private static KNXDataType GetKnxDataType(string objectSize)
        {
            KNXDataType type = KNXDataType.Bit1;

            switch (objectSize)
            {
                case "1 Bit":
                    type = KNXDataType.Bit1;
                    break;
                case "2 Bit":
                    type = KNXDataType.Bit2;
                    break;
                case "3 Bit":
                    type = KNXDataType.Bit3;
                    break;
                case "4 Bit":
                    type = KNXDataType.Bit4;
                    break;
                case "5 Bit":
                    type = KNXDataType.Bit5;
                    break;
                case "6 Bit":
                    type = KNXDataType.Bit6;
                    break;
                case "7 Bit":
                    type = KNXDataType.Bit7;
                    break;
                case "1 Byte":
                    type = KNXDataType.Bit8;
                    break;
                case "2 Bytes":
                    type = KNXDataType.Bit16;
                    break;
                case "3 Bytes":
                    type = KNXDataType.Bit24;
                    break;
                case "4 Bytes":
                    type = KNXDataType.Bit32;
                    break;
                case "6 Bytes":
                    type = KNXDataType.Bit48;
                    break;
                case "8 Bytes":
                    type = KNXDataType.Bit64;
                    break;
                case "10 Bytes":
                    type = KNXDataType.Bit80;
                    break;
                case "14 Bytes":
                    type = KNXDataType.Byte14;
                    break;
                default:
                    MessageBox.Show("没有匹配的数据类型！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return type;
        }
        #endregion
    }
}
