
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
        public const string MyKey = "guokaile@163.com";

        public const string ResourceFile = "Resources.resx";
        public const string ResourceNameDatapointType = "DatapointType";


        // 属性显示表格的列索引
        public const int NameColumn = 0;
        public const int ValueColumn = 1;
        public const int ButtonColumn = 2;

        //
        public const string PropType = "对象类型";
        public const string PropTitle = "标题";
        public const string PorpRowCount = "表格中的行数";
        public const string PropColumnCount = "表格中的列数";
        public const string PropRow = "行位置";
        public const string PropColumn = "列位置";
        public const string PropRowSpan = "行跨度";
        public const string PropColumnSpan = "列跨度";
        public const string PropEtsWriteAddressIds = "ETS写地址表";
        public const string PropEtsReadAddressId = "ETS读地址";
        public const string PropBackColor = "背景色";
        public const string PropForeColor = "前景色";
        public const string PropLeftImage = "左侧图片";
        public const string PropRightImage = "右侧图片";
        public const string PropSlideImage = "滑动图片";
        public const string PropMin = "最小值";
        public const string PropMax = "最大值";
        public const string PropControlSymbol = "两侧的图标";
        public const string PropSendInterval = "延迟时间(毫秒)";
        public const string PropLeftText = "左标签";
        public const string PropRightText = "右标签";
        public const string PropHasTip = "是否有提示";
        public const string PropTip = "提示文字";

        // 文件后缀名
        public const string KnxUiEditorFileExt = "knxuie";

        public const string PicFilter =
            "PNG图片 (*.png)|*.png|jpg图片 (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|图片文件(*.bmp;*.jpg;*.png;*.ioc)|*.bmp;*.jpg;*.png;*.ioc|All files (*.*)|*.*";


        public static class View
        {
            public const string KnxAppType = "KNXApp";
            public const string KnxAreaType = "KNXArea";
            public const string KnxRoomType = "KNXRoom";
            public const string KnxPageType = "KNXPage";
            public const string KnxGridType = "KNXGrid";
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
        }

    }
}
