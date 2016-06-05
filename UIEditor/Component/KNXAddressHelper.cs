using Structure;
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
                    type = KNXDataType.Bit112;
                    break;
                default:
                    type = KNXDataType.None;
                    MessageBox.Show(ResourceMng.GetString("Message41"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return type;
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
                    MessageBox.Show(ResourceMng.GetString("Message41"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
