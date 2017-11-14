using KNX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Component
{
    public class KNXAddressHelper
    {
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

        public static KNXDataType GetKnxDataType(string objectSize)
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
                case "2 Byte":
                    type = KNXDataType.Bit16;
                    break;
                case "3 Bytes":
                case "3 Byte":
                    type = KNXDataType.Bit24;
                    break;
                case "4 Bytes":
                case "4 Byte":
                    type = KNXDataType.Bit32;
                    break;
                case "6 Bytes":
                case "6 Byte":
                    type = KNXDataType.Bit48;
                    break;
                case "8 Bytes":
                case "8 Byte":
                    type = KNXDataType.Bit64;
                    break;
                case "10 Bytes":
                case "10 Byte":
                    type = KNXDataType.Bit80;
                    break;
                case "14 Bytes":
                case "14 Byte":
                    type = KNXDataType.Bit112;
                    break;
                default:
                    type = KNXDataType.None;
                    //MessageBox.Show(UIResMang.GetString("Message41"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(objectSize + " ===> " + UIResMang.GetString("Message41"));
                    break;
            }

            return type;
        }

        public static string GetSize(KNXDataType type)
        {
            string size = "";
            switch (type)
            {
                case KNXDataType.Bit1:
                    size = "1 Bit";
                    break;
                case KNXDataType.Bit2:
                    size = "2 Bit";
                    break;
                case KNXDataType.Bit3:
                    size = "3 Bit";
                    break;
                case KNXDataType.Bit4:
                    size = "4 Bit";
                    break;
                case KNXDataType.Bit5:
                    size = "5 Bit";
                    break;
                case KNXDataType.Bit6:
                    size = "6 Bit";
                    break;
                case KNXDataType.Bit7:
                    size = "7 Bit";
                    break;
                case KNXDataType.Bit8:
                    size = "1 Byte";
                    break;
                case KNXDataType.Bit16:
                    size = "2 Bytes";
                    break;
                case KNXDataType.Bit24:
                    size = "3 Bytes";
                    break;
                case KNXDataType.Bit32:
                    size = "4 Bytes";
                    break;
                case KNXDataType.Bit48:
                    size = "6 Bytes";
                    break;
                case KNXDataType.Bit64:
                    size = "8 Bytes";
                    break;
                case KNXDataType.Bit80:
                    size = "10 Bytes";
                    break;
                case KNXDataType.Bit112:
                    size = "14 Bytes";
                    break;
                default:
                    MessageBox.Show(UIResMang.GetString("Message41"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return size;
        }

        public static KNXDataType GetDPTType(string sizeInBit)
        {
            KNXDataType type = KNXDataType.Bit1;

            switch (sizeInBit)
            {
                case "1":
                    type = KNXDataType.Bit1;
                    break;
                case "2":
                    type = KNXDataType.Bit2;
                    break;
                case "4":
                    type = KNXDataType.Bit4;
                    break;
                case "8":
                    type = KNXDataType.Bit8;
                    break;
                case "16":
                    type = KNXDataType.Bit16;
                    break;
                case "24":
                    type = KNXDataType.Bit24;
                    break;
                case "32":
                    type = KNXDataType.Bit32;
                    break;
                case "48":
                    type = KNXDataType.Bit48;
                    break;
                case "64":
                    type = KNXDataType.Bit64;
                    break;
                case "80":
                    type = KNXDataType.Bit80;
                    break;
                case "112":
                    type = KNXDataType.Bit112;
                    break;
                default:
                    MessageBox.Show(UIResMang.GetString("Message41"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return type;
        }

        public static KNXPriority GetKNXPriority(string strPriority)
        {
            KNXPriority priority;

            switch (strPriority)
            {
                case "Alert":
                    priority = KNXPriority.Urgent;
                    break;
                case "Hight":
                    priority = KNXPriority.Normal;
                    break;
                case "Low":
                    priority = KNXPriority.Low;
                    break;
                case "system":
                    priority = KNXPriority.System;
                    break;
                default:
                    priority = KNXPriority.Low;
                    break;
            }

            return priority;
        }
    }
}
