
namespace UIEditor.Action {

    /// <summary>
    /// 组地址行为
    /// </summary>
    public class GroupAddressAction {

        public string name { get; set; }
    }

    /// <summary>
    /// 组地址大小为1个bit时的行为
    /// </summary>
    public class Bit1Action :GroupAddressAction {
        
        public int defaultValue {get; set;}
    }
}