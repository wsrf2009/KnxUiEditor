
namespace UIEditor
{
    public static class MyConst
    {
        public const char SplitSymbol = ',';

        // 临时目录名称
        public const string AppDataFolder = @"AppData";
        public const string TempFolder = @"temp";
        public const string ImageFolder = @"res\img";

        public const string DefaultIcon = "default_icon.png";
        public const string SwitchIcon = @"Resources\controls\switch_16x16.png";

        // 文件版本信息
        public const string VersionFile = "Version.json";

        // 组地址文件名
        public const string GroupAddressFile = "GroupAddress.json";

        // 界面定义数据文件名
        public const string KnxUiMetaDataFile = "KnxUiMetaData.json";

        // 
        public const string ConnectionsFile = "Connections.json";

        // 默认的项目名称
        public const string DefaultKnxUiProjectName = "KnxProject1.knxuie";

        // key
        //public const string MyKey = "guokaile@163.com";
        public const string MyKey = "com.sation.knxcontroller.uieditor";

        public const string ResourceFile = "Resources.resx";
        public const string ResourceNameDatapointType = "DatapointType";

        public const string XmlTagAppLanguange = "Localize";

        //public const string TextApplication = "应用程序";
        //public const string TextArea = "区域";
        //public const string TextPage = "页面";
        //public const string TextRoom = "房间";
        //public const string TextGroupBox = "组框";


        // 属性显示表格的列索引
        public const int NameColumn = 0;
        public const int ValueColumn = 1;
        public const int ButtonColumn = 2;

        //
        //public const string PropType = "对象类型";
        //public const string PropTitle = "标题";
        //public const string PropX = "X(水平起始)";
        //public const string PropY = "Y(垂直起始)";
        //public const string PropWidth = "宽度(像素)";
        //public const string PropHeight = "高度(像素)";
        //public const string PropAlpha = "不透明度";
        //public const string PropRadius = "圆角半径";
        //public const string PropFlatStyle = "平面样式";
        //public const string PropBackColor = "背景色";
        //public const string PropBackgroundImage = "背景图片";
        //public const string PropFontColor = "字体颜色";
        //public const string PropFontSize = "字体大小";

        //public const string PropEtsWriteAddressIds = "ETS写地址表";
        //public const string PropEtsReadAddressId = "ETS读地址";
        //public const string PropHasTip = "是否有提示";
        //public const string PropTip = "提示文字";
        //public const string PropClickable = "控件是否可以点击";

        //public const string TextSwitch = "开关";
        //public const string SwitchStateOn = "开";
        //public const string SwitchStateOff = "关";
        //public const string PropSwitchImageOn = "开启时显示的图片";
        //public const string PropSwitchImageOff = "关闭时显示的图片";
        //public const string PropSwitchColorOn = "开启时控件背景色";
        //public const string PropSwitchColorOff = "关闭时控件背景色";

        //public const string TextBlinds = "窗帘控制";

        //public const string TextLabel = "标签";

        //public const string TextSliderSwitch = "滑动开关";
        //public const string PropLeftImage = "左侧图片";
        //public const string PropRightImage = "右侧图片";
        //public const string PropSlideImage = "滑动图片";
        ////public const string PropControlSymbol = "两侧的图标";
        ////public const string PropSendInterval = "延迟时间(毫秒)";

        //public const string TextSceneButton = "场景按钮";
        //public const string PropIsGroup = "是否属于组";
        //public const string PropDefaultValue = "默认值";

        //public const string PropLeftText = "左标签";
        //public const string PropRightText = "右标签";

        //public const string TextValueDisplay = "数据显示";
        //public const string PropMeasurementUnit = "计量单位";
        //public const string PropDisplayValue = "显示值";
        //public const string PropDecimalDigits = "小数位数";

        //public const string TextTimer = "定时器";
        //public const string PropIcon = "图标";

        //public const string TextDigitalAdjustment = "数字调节";
        //public const string PropDigitalNumber = "数字位数";
        //public const string PropMax = "最大值";
        //public const string PropMin = "最小值";

        //public const string TextRadioGroup = "单选按钮组框";

        // 文件后缀名
        public const string KnxUiEditorFileExt = "knxuie";

        public const string PicFilter =
            "PNG (*.png)|*.png|JPG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|Picture Files (*.bmp;*.jpg;*.png;*.ioc)|*.bmp;*.jpg;*.png;*.ioc|All files (*.*)|*.*";


        public static class View
        {
            public const string KnxAppType = "KNXApp";
            public const string KnxAreaType = "KNXArea";
            public const string KnxRoomType = "KNXRoom";
            public const string KnxPageType = "KNXPage";
        }

        public static class Controls
        {
            public const string KnxBlindsType = "KNXBlinds";
            public const string KnxColorLightType = "KNXColorLight";
            public const string KnxLabelType = "KNXLabel";
            public const string KnxMediaButtonType = "KNXMediaButton";
            public const string KnxSceneButtonType = "KNXSceneButton";
            public const string KnxSipCallType = "KNXSIPCall";
            public const string KnxSliderType = "KNXSlider";
            public const string KnxSliderSwitchType = "KNXSliderSwitch";
            public const string KnxSnapperType = "KNXSnapper";
            public const string KnxSnapperSwitchType = "KNXSnapperSwitch";
            public const string KnxSwitchType = "KNXSwitch";
            public const string KnxValueDisplayType = "KNXValueDisplay";
            public const string KnxWebCamViewerType = "KNXWebcamViewer";
            public const string KnxImageButtonType = "KNXImageButton";
            public const string KnxTimerButtonType = "KNXTimerButton";
            public const string KnxTimerTaskListViewType = "KNXTimerTaskListView";
            public const string KnxDigitalAdjustment = "KNXDigitalAdjustment";
            //public const string KnxRadioGroup = "KNXRadioGroup";
            public const string KnxGroupBoxType = "KNXGroupBox";
        }

    }
}
