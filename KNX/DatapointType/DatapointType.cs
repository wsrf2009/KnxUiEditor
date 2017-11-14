using System.Collections.Generic;
using System.Windows.Forms;
using KNX.DatapointAction;
using KNX.DatapointType.Type24TimesChannelActivation;
using KNX.DatapointType.Type2NibbleSet;
using KNX.DatapointType.Type3ByteColourRGB;
using KNX.DatapointType.Type3x2ByteFloatValue;
using KNX.DatapointType.Type411ByteCombinedInformation;
using KNX.DatapointType.TypeB16;
using KNX.DatapointType.TypeB16ConfigurationDiagnostics;
using KNX.DatapointType.TypeB24;
using KNX.DatapointType.TypeB32;
using KNX.DatapointType.TypeB8;
using KNX.DatapointType.TypeB8ConfigurationDiagnostics;
using KNX.DatapointType.TypeDatapointTypeVersion;
using KNX.DatapointType.TypeDate;
using KNX.DatapointType.TypeDPTAccessData;
using KNX.DatapointType.TypeDPTAlarmInfo;
using KNX.DatapointType.TypeDPTDateTime;
using KNX.DatapointType.TypeDPTSceneControl;
using KNX.DatapointType.TypeDPTSceneInfo;
using KNX.DatapointType.TypeElectricalEnergy;
using KNX.DatapointType.TypeLanguageCodeISO6391;
using KNX.DatapointType.TypeMBusAddress;
using KNX.DatapointType.Types2OctetFloatValue;
using KNX.DatapointType.Types2OctetSignedValue;
using KNX.DatapointType.Types2OctetUnsignedValue;
using KNX.DatapointType.Types4OctetFloatValue;
using KNX.DatapointType.Types4OctetSignedValue;
using KNX.DatapointType.Types4OctetUnsignedValue;
using KNX.DatapointType.Types8BitUnsignedValue;
using KNX.DatapointType.TypesB1;
using KNX.DatapointType.TypesB1U3;
using KNX.DatapointType.TypesB2;
using KNX.DatapointType.TypeSceneNumber;
using KNX.DatapointType.TypesCharacterSet;
using KNX.DatapointType.TypesN2;
using KNX.DatapointType.TypesN8;
using KNX.DatapointType.TypesString;
using KNX.DatapointType.TypesV8;
using KNX.DatapointType.TypeTime;

namespace KNX.DatapointType
{
    public class DatapointType : TreeNode
    {
        #region Datapoint Main Number
        public const string DPT_1 = "1";
        public const string DPT_2 = "2";
        public const string DPT_3 = "3";
        public const string DPT_4 = "4";
        public const string DPT_5 = "5";
        public const string DPT_6 = "6";
        public const string DPT_7 = "7";
        public const string DPT_8 = "8";
        public const string DPT_9 = "9";
        public const string DPT_10 = "10";
        public const string DPT_11 = "11";
        public const string DPT_12 = "12";
        public const string DPT_13 = "13";
        public const string DPT_14 = "14";
        public const string DPT_15 = "15";
        public const string DPT_16 = "16";
        public const string DPT_17 = "17";
        public const string DPT_18 = "18";
        public const string DPT_19 = "19";
        public const string DPT_20 = "20";
        public const string DPT_21 = "21";
        public const string DPT_22 = "22";
        public const string DPT_23 = "23";
        public const string DPT_25 = "25";
        public const string DPT_26 = "26";
        public const string DPT_27 = "27";
        public const string DPT_29 = "29";
        public const string DPT_30 = "30";
        public const string DPT_206 = "206";
        public const string DPT_217 = "217";
        public const string DPT_219 = "219";
        public const string DPT_222 = "222";
        public const string DPT_229 = "229";
        public const string DPT_230 = "230";
        public const string DPT_232 = "232";
        public const string DPT_234 = "234";
        public const string DPT_237 = "237";
        public const string DPT_238 = "238";
        #endregion

        #region Datapoint Sub Number
        public const string DPST_ANY = "*";
        public const string DPST_0 = "000";
        public const string DPST_1 = "001";
        public const string DPST_2 = "002";
        public const string DPST_3 = "003";
        public const string DPST_4 = "004";
        public const string DPST_5 = "005";
        public const string DPST_6 = "006";
        public const string DPST_7 = "007";
        public const string DPST_8 = "008";
        public const string DPST_9 = "009";
        public const string DPST_10 = "010";
        public const string DPST_11 = "011";
        public const string DPST_12 = "012";
        public const string DPST_13 = "013";
        public const string DPST_14 = "014";
        public const string DPST_15 = "015";
        public const string DPST_16 = "016";
        public const string DPST_17 = "017";
        public const string DPST_18 = "018";
        public const string DPST_19 = "019";
        public const string DPST_20 = "020";
        public const string DPST_21 = "021";
        public const string DPST_22 = "022";
        public const string DPST_23 = "023";
        public const string DPST_24 = "024";
        public const string DPST_25 = "025";
        public const string DPST_26 = "026";
        public const string DPST_27 = "027";
        public const string DPST_28 = "028";
        public const string DPST_29 = "029";
        public const string DPST_30 = "030";
        public const string DPST_31 = "031";
        public const string DPST_32 = "032";
        public const string DPST_33 = "033";
        public const string DPST_34 = "034";
        public const string DPST_35 = "035";
        public const string DPST_36 = "036";
        public const string DPST_37 = "037";
        public const string DPST_38 = "038";
        public const string DPST_39 = "039";
        public const string DPST_40 = "040";
        public const string DPST_41 = "041";
        public const string DPST_42 = "042";
        public const string DPST_43 = "043";
        public const string DPST_44 = "044";
        public const string DPST_45 = "045";
        public const string DPST_46 = "046";
        public const string DPST_47 = "047";
        public const string DPST_48 = "048";
        public const string DPST_49 = "049";
        public const string DPST_50 = "050";
        public const string DPST_51 = "051";
        public const string DPST_52 = "052";
        public const string DPST_53 = "053";
        public const string DPST_54 = "054";
        public const string DPST_55 = "055";
        public const string DPST_56 = "056";
        public const string DPST_57 = "057";
        public const string DPST_58 = "058";
        public const string DPST_59 = "059";
        public const string DPST_60 = "060";
        public const string DPST_61 = "061";
        public const string DPST_62 = "062";
        public const string DPST_63 = "063";
        public const string DPST_64 = "064";
        public const string DPST_65 = "065";
        public const string DPST_66 = "066";
        public const string DPST_67 = "067";
        public const string DPST_68 = "068";
        public const string DPST_69 = "069";
        public const string DPST_70 = "070";
        public const string DPST_71 = "071";
        public const string DPST_72 = "072";
        public const string DPST_73 = "073";
        public const string DPST_74 = "074";
        public const string DPST_75 = "075";
        public const string DPST_76 = "076";
        public const string DPST_77 = "077";
        public const string DPST_78 = "078";
        public const string DPST_79 = "079";
        public const string DPST_100 = "100";
        public const string DPST_101 = "101";
        public const string DPST_102 = "102";
        public const string DPST_103 = "103";
        public const string DPST_104 = "104";
        public const string DPST_105 = "105";
        public const string DPST_106 = "106";
        public const string DPST_107 = "107";
        public const string DPST_108 = "108";
        public const string DPST_109 = "109";
        public const string DPST_110 = "110";
        public const string DPST_111 = "111";
        public const string DPST_112 = "112";
        public const string DPST_113 = "113";
        public const string DPST_114 = "114";
        public const string DPST_120 = "120";
        public const string DPST_121 = "121";
        public const string DPST_122 = "122";
        public const string DPST_600 = "600";
        public const string DPST_601 = "601";
        public const string DPST_602 = "602";
        public const string DPST_603 = "603";
        public const string DPST_604 = "604";
        public const string DPST_605 = "605";
        public const string DPST_606 = "606";
        public const string DPST_607 = "607";
        public const string DPST_608 = "608";
        public const string DPST_609 = "609";
        public const string DPST_610 = "610";
        public const string DPST_801 = "801";
        public const string DPST_802 = "802";
        public const string DPST_803 = "803";
        public const string DPST_804 = "804";
        public const string DPST_1000 = "1000";
        public const string DPST_1001 = "1001";
        public const string DPST_1002 = "1002";
        public const string DPST_1003 = "1003";
        public const string DPST_1010 = "1010";

        #endregion

        public string KNXMainNumber { get; set; }

        public string KNXSubNumber { get; set; }

        public string DPTName { get; set; }

        public KNXDataType Type { get; set; }

        //public List<DatapointAction.DatapointActionNode> Actions { get; set; }

        public DatapointType()
        {
            this.KNXMainNumber = DPT_1;
            this.KNXSubNumber = "*";
            this.DPTName = "";
            this.Type = KNXDataType.Bit1;

            this.Text = "";
        }

        public static List<TreeNode> GetAllTypeNodes()
        {
            List<TreeNode> listTypeNodes = new List<TreeNode>();

            listTypeNodes.Add(TypesB1Node.GetAllTypeNode());
            listTypeNodes.Add(TypesB2Node.GetAllTypeNode());
            listTypeNodes.Add(TypesB1U3Node.GetAllTypeNode());
            listTypeNodes.Add(TypesCharacterSetNode.GetAllTypeNode());
            listTypeNodes.Add(Types8BitUnsignedValueNode.GetAllTypeNode());
            listTypeNodes.Add(TypesV8Node.GetAllTypeNode());
            listTypeNodes.Add(Types2OctetUnsignedValueNode.GetAllTypeNode());
            listTypeNodes.Add(Types2OctetSignedValueNode.GetAllTypeNode());
            listTypeNodes.Add(Types2OctetFloatValueNode.GetAllTypeNode());
            listTypeNodes.Add(TypeTimeNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDateNode.GetAllTypeNode());
            listTypeNodes.Add(Types4OctetUnsignedValueNode.GetAllTypeNode());
            listTypeNodes.Add(Types4OctetSignedValueNode.GetAllTypeNode());
            listTypeNodes.Add(Types4OctetFloatValueNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDPTAccessDataNode.GetAllTypeNode());
            listTypeNodes.Add(TypesStringNode.GetAllTypeNode());
            listTypeNodes.Add(TypeSceneNumberNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDPTSceneControlNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDPTDateTimeNode.GetAllTypeNode());
            listTypeNodes.Add(TypesN8Node.GetAllTypeNode());
            listTypeNodes.Add(TypeB8Node.GetAllTypeNode());
            listTypeNodes.Add(TypeB16Node.GetAllTypeNode());
            listTypeNodes.Add(TypesN2Node.GetAllTypeNode());
            listTypeNodes.Add(Type2NibbleSetNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDPTSceneInfoNode.GetAllTypeNode());
            listTypeNodes.Add(TypeB32Node.GetAllTypeNode());
            listTypeNodes.Add(TypeElectricalEnergyNode.GetAllTypeNode());
            listTypeNodes.Add(Type24TimesChannelActivationNode.GetAllTypeNode());
            listTypeNodes.Add(TypeB24Node.GetAllTypeNode());
            listTypeNodes.Add(TypeDatapointTypeVersionNode.GetAllTypeNode());
            listTypeNodes.Add(TypeDPTAlarmInfoNode.GetAllTypeNode());
            listTypeNodes.Add(Type3x2ByteFloatValueNode.GetAllTypeNode());
            listTypeNodes.Add(Type411ByteCombinedInformationNode.GetAllTypeNode());
            listTypeNodes.Add(TypeMBusAddressNode.GetAllTypeNode());
            listTypeNodes.Add(Type3ByteColourRGBNode.GetAllTypeNode());
            listTypeNodes.Add(TypeLanguageCodeISO6391Node.GetAllTypeNode());
            listTypeNodes.Add(TypeB16ConfigurationDiagnosticsNode.GetAllTypeNode());
            listTypeNodes.Add(TypeB8ConfigurationDiagnosticsNode.GetAllTypeNode());

            return listTypeNodes;
        }

        public static List<TreeNode> GetAllActionNodes()
        {
            List<TreeNode> listActionNodes = new List<TreeNode>();
            listActionNodes.Add(TypesB1Node.GetAllActionNode());
            listActionNodes.Add(TypesB1U3Node.GetAllActionNode());
            listActionNodes.Add(Types8BitUnsignedValueNode.GetAllActionNode());
            listActionNodes.Add(TypeDPTSceneControlNode.GetAllActionNode());

            return listActionNodes;
        }

        public static Dictionary<int, string> GetDPTMainNumber()
        {
            Dictionary<int, string> dicMainNumber = new Dictionary<int, string>();
            dicMainNumber.Add(1, DPT_1);
            dicMainNumber.Add(2, DPT_2);
            dicMainNumber.Add(3, DPT_3);
            dicMainNumber.Add(4, DPT_4);
            dicMainNumber.Add(5, DPT_5);
            dicMainNumber.Add(6, DPT_6);
            dicMainNumber.Add(7, DPT_7);
            dicMainNumber.Add(8, DPT_8);
            dicMainNumber.Add(9, DPT_9);
            dicMainNumber.Add(10, DPT_10);
            dicMainNumber.Add(11, DPT_11);
            dicMainNumber.Add(12, DPT_12);
            dicMainNumber.Add(13, DPT_13);
            dicMainNumber.Add(14, DPT_14);
            dicMainNumber.Add(15, DPT_15);
            dicMainNumber.Add(16, DPT_16);
            dicMainNumber.Add(17, DPT_17);
            dicMainNumber.Add(18, DPT_18);
            dicMainNumber.Add(19, DPT_19);
            dicMainNumber.Add(20, DPT_20);
            dicMainNumber.Add(21, DPT_21);
            dicMainNumber.Add(22, DPT_22);
            dicMainNumber.Add(23, DPT_23);
            dicMainNumber.Add(25, DPT_25);
            dicMainNumber.Add(26, DPT_26);
            dicMainNumber.Add(27, DPT_27);
            dicMainNumber.Add(29, DPT_29);
            dicMainNumber.Add(30, DPT_30);
            dicMainNumber.Add(206, DPT_206);
            dicMainNumber.Add(217, DPT_217);
            dicMainNumber.Add(219, DPT_219);
            dicMainNumber.Add(222, DPT_222);
            dicMainNumber.Add(229, DPT_229);
            dicMainNumber.Add(230, DPT_230);
            dicMainNumber.Add(232, DPT_232);
            dicMainNumber.Add(234, DPT_234);
            dicMainNumber.Add(237, DPT_237);
            dicMainNumber.Add(238, DPT_238);

            return dicMainNumber;
        }

        public static Dictionary<int, string> GetDPTSubNumber()
        {
            Dictionary<int, string> dicSubNumber = new Dictionary<int, string>();
            dicSubNumber.Add(0, DPST_0);
            dicSubNumber.Add(1, DPST_1);
            dicSubNumber.Add(2, DPST_2);
            dicSubNumber.Add(3, DPST_3);
            dicSubNumber.Add(4, DPST_4);
            dicSubNumber.Add(5, DPST_5);
            dicSubNumber.Add(6, DPST_6);
            dicSubNumber.Add(7, DPST_7);
            dicSubNumber.Add(8, DPST_8);
            dicSubNumber.Add(9, DPST_9);
            dicSubNumber.Add(10, DPST_10);
            dicSubNumber.Add(11, DPST_11);
            dicSubNumber.Add(12, DPST_12);
            dicSubNumber.Add(13, DPST_13);
            dicSubNumber.Add(14, DPST_14);
            dicSubNumber.Add(15, DPST_15);
            dicSubNumber.Add(16, DPST_16);
            dicSubNumber.Add(17, DPST_17);
            dicSubNumber.Add(18, DPST_18);
            dicSubNumber.Add(19, DPST_19);
            dicSubNumber.Add(20, DPST_20);
            dicSubNumber.Add(21, DPST_21);
            dicSubNumber.Add(22, DPST_22);
            dicSubNumber.Add(23, DPST_23);
            dicSubNumber.Add(24, DPST_24);
            dicSubNumber.Add(25, DPST_25);
            dicSubNumber.Add(26, DPST_26);
            dicSubNumber.Add(27, DPST_27);
            dicSubNumber.Add(28, DPST_28);
            dicSubNumber.Add(29, DPST_29);
            dicSubNumber.Add(30, DPST_30);
            dicSubNumber.Add(31, DPST_31);
            dicSubNumber.Add(32, DPST_32);
            dicSubNumber.Add(33, DPST_33);
            dicSubNumber.Add(34, DPST_34);
            dicSubNumber.Add(35, DPST_35);
            dicSubNumber.Add(36, DPST_36);
            dicSubNumber.Add(37, DPST_37);
            dicSubNumber.Add(38, DPST_38);
            dicSubNumber.Add(39, DPST_39);
            dicSubNumber.Add(40, DPST_40);
            dicSubNumber.Add(41, DPST_41);
            dicSubNumber.Add(42, DPST_42);
            dicSubNumber.Add(43, DPST_43);
            dicSubNumber.Add(44, DPST_44);
            dicSubNumber.Add(45, DPST_45);
            dicSubNumber.Add(46, DPST_46);
            dicSubNumber.Add(47, DPST_47);
            dicSubNumber.Add(48, DPST_48);
            dicSubNumber.Add(49, DPST_49);
            dicSubNumber.Add(50, DPST_50);
            dicSubNumber.Add(51, DPST_51);
            dicSubNumber.Add(52, DPST_52);
            dicSubNumber.Add(53, DPST_53);
            dicSubNumber.Add(54, DPST_54);
            dicSubNumber.Add(55, DPST_55);
            dicSubNumber.Add(56, DPST_56);
            dicSubNumber.Add(57, DPST_57);
            dicSubNumber.Add(58, DPST_58);
            dicSubNumber.Add(59, DPST_59);
            dicSubNumber.Add(60, DPST_60);
            dicSubNumber.Add(61, DPST_61);
            dicSubNumber.Add(62, DPST_62);
            dicSubNumber.Add(63, DPST_63);
            dicSubNumber.Add(64, DPST_64);
            dicSubNumber.Add(65, DPST_65);
            dicSubNumber.Add(66, DPST_66);
            dicSubNumber.Add(67, DPST_67);
            dicSubNumber.Add(68, DPST_68);
            dicSubNumber.Add(69, DPST_69);
            dicSubNumber.Add(70, DPST_70);
            dicSubNumber.Add(71, DPST_71);
            dicSubNumber.Add(72, DPST_72);
            dicSubNumber.Add(73, DPST_73);
            dicSubNumber.Add(74, DPST_74);
            dicSubNumber.Add(75, DPST_75);
            dicSubNumber.Add(76, DPST_76);
            dicSubNumber.Add(77, DPST_77);
            dicSubNumber.Add(78, DPST_78);
            dicSubNumber.Add(79, DPST_79);
            dicSubNumber.Add(100, DPST_100);
            dicSubNumber.Add(101, DPST_101);
            dicSubNumber.Add(102, DPST_102);
            dicSubNumber.Add(103, DPST_103);
            dicSubNumber.Add(104, DPST_104);
            dicSubNumber.Add(105, DPST_105);
            dicSubNumber.Add(106, DPST_106);
            dicSubNumber.Add(107, DPST_107);
            dicSubNumber.Add(108, DPST_108);
            dicSubNumber.Add(109, DPST_109);
            dicSubNumber.Add(110, DPST_110);
            dicSubNumber.Add(111, DPST_111);
            dicSubNumber.Add(112, DPST_112);
            dicSubNumber.Add(113, DPST_113);
            dicSubNumber.Add(114, DPST_114);
            dicSubNumber.Add(120, DPST_120);
            dicSubNumber.Add(121, DPST_121);
            dicSubNumber.Add(122, DPST_122);
            dicSubNumber.Add(600, DPST_600);
            dicSubNumber.Add(601, DPST_601);
            dicSubNumber.Add(602, DPST_602);
            dicSubNumber.Add(603, DPST_603);
            dicSubNumber.Add(604, DPST_604);
            dicSubNumber.Add(605, DPST_605);
            dicSubNumber.Add(606, DPST_606);
            dicSubNumber.Add(607, DPST_607);
            dicSubNumber.Add(608, DPST_608);
            dicSubNumber.Add(609, DPST_609);
            dicSubNumber.Add(610, DPST_610);
            dicSubNumber.Add(801, DPST_801);
            dicSubNumber.Add(802, DPST_802);
            dicSubNumber.Add(803, DPST_803);
            dicSubNumber.Add(804, DPST_804);
            dicSubNumber.Add(1000, DPST_1000);
            dicSubNumber.Add(1001, DPST_1001);
            dicSubNumber.Add(1002, DPST_1002);
            dicSubNumber.Add(1003, DPST_1003);
            dicSubNumber.Add(1010, DPST_1010);

            return dicSubNumber;
        }
    }
}
