
namespace UIEditor.Component
{
    public static class MyConst
    {
        public const string ProjectName = "UIEditor";
        public const char SplitSymbol = ',';

        // 临时目录名称
        public const string ProjFolder = @"Proj";
        public const string ResFolder = @"res";
        public const string ImgFolder = @"img";
        public const string CollFolder = @"coll";

        public const string TempFolder = @"temp";

        public const string DefaultIcon = "default_icon.png";
        //public const string SwitchIcon = @"Resources\controls\switch_16x16.png";

        // 文件版本信息
        public const string VersionFile = "Version.json";

        // 组地址文件名
        public const string GroupAddressFile = "GroupAddress.json";

        // 界面定义数据文件名
        public const string KnxUiMetaDataFile = "KnxUiMetaData.json";

        // 模板界面文件名
        public const string TemplateMetaFile = "TemplateMeta.json";

        // 模板版本文件名
        public const string TemplateVersion = "TemplateVersion.json";

        /// <summary>
        /// 模板预览图片文件名
        /// </summary>
        public const string TemplatePreviewFile = "Preview.png";

        // 
        public const string ConnectionsFile = "Connections.json";

        // 默认的项目名称
        public const string DefaultKnxUiProjectName = "KnxUiProject" + "." + KnxUiEditorFileExt;

        // key
        public const string MyKey = "com.sation.knxcontroller.uieditor";

        public const string ResourceFile = "Resources.resx";
        public const string ResourceNameDatapointType = "DatapointType";

        public const string XmlTagAppLanguange = "Localize";
        public const string XmlTagRuler = "Ruler";

        // 属性显示表格的列索引
        public const int NameColumn = 0;
        public const int ValueColumn = 1;
        public const int ButtonColumn = 2;

        // 文件后缀名
        public const string KnxUiEditorFileExt = "knxuie";

        public const string PicFilter = "PNG (*.png)|*.png|All files (*.*)|*.*";
//            "PNG (*.png)|*.png|JPG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|Picture Files (*.bmp;*.jpg;*.png;*.ioc)|*.bmp;*.jpg;*.png;*.ioc|All files (*.*)|*.*";


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
            public const string KnxShutterType = "KNXShutter";
            //public const string KnxColorLightType = "KNXColorLight";
            public const string KnxLabelType = "KNXLabel";
            public const string KnxMediaButtonType = "KNXMediaButton";
            public const string KnxSceneButtonType = "KNXSceneButton";
            //public const string KnxSipCallType = "KNXSIPCall";
            //public const string KnxSliderType = "KNXSlider";
            public const string KnxSliderSwitchType = "KNXSliderSwitch";
            public const string KnxDimmerType = "KNXDimmer";
            //public const string KnxSnapperType = "KNXSnapper";
            //public const string KnxSnapperSwitchType = "KNXSnapperSwitch";
            public const string KnxSwitchType = "KNXSwitch";
            public const string KnxValueDisplayType = "KNXValueDisplay";
            public const string KnxWebCamViewerType = "KNXWebCamer";
            public const string KnxImageButtonType = "KNXImageButton";
            public const string KnxTimerButtonType = "KNXTimerButton";
            //public const string KnxTimerTaskListViewType = "KNXTimerTaskListView";
            public const string KnxDigitalAdjustmentType = "KNXDigitalAdjustment";
            //public const string KnxRadioGroup = "KNXRadioGroup";
            public const string KnxGroupBoxType = "KNXGroupBox";
            public const string KnxAirConditionType = "KNXAirCondition";
            public const string KnxHVACType = "KNXHVAC";
        }

        public const string TemplateFilterControl = @"Control Files (*.blinds;*label;*scene;*.sliderswitch;*.switch;*.valuedisplay;*.imagebutton;*.timerbutton;*.digitaladjustment;*.groupbox)|*.blinds;*label;*scene;*.sliderswitch;*.switch;*.valuedisplay;*.imagebutton;*.timerbutton;*.digitaladjustment;*.groupbox|Blinds (*.blinds)|*.blinds|Label (*.label)|*.label|SceneButton (*.scene)|*.scene|SliderSwitch (*.sliderswitch)|*.sliderswitch|Switch (*.switch)|*.switch|ValueDisplay (*.valuedisplay)|*.valuedisplay|ImageButton (*.imagebutton)|*.imagebutton|TimerButton (*.timerbutton)|*.timerbutton|DigitalAdjustment (*.digitaladjustment)|*.digitaladjustment|GroupBox (*.groupbox)|*.groupbox|All files (*.*)|*.*";
        public const string TemplateFilterBlinds = @"Blinds (*.blinds)|*.blinds";
        public const string TemplateFilterLabel = @"Label (*.label)|*.label";
        public const string TemplateFilterSceneButton = @"SceneButton (*.scene)|*.scene";
        public const string TemplateFilterSliderSwitch = @"SliderSwitch (*.sliderswitch)|*.sliderswitch";
        public const string TemplateFilterSwitch = @"Switch (*.switch)|*.switch";
        public const string TemplateFilterValueDisplay = @"ValueDisplay (*.valuedisplay)|*.valuedisplay";
        public const string TemplateFilterImageButton = @"ImageButton (*.imagebutton)|*.imagebutton";
        public const string TemplateFilterTimerButton = @"TimerButton (*.timerbutton)|*.timerbutton";
        public const string TemplateFilterDigitalAdjustment = @"DigitalAdjustment (*.digitaladjustment)|*.digitaladjustment";
        public const string TemplateFilterGroupBox = @"GroupBox (*.groupbox)|*.groupbox";

        public const string TemplateExtBlinds = @".blinds";
        public const string TemplateExtLabel = @".label";
        public const string TemplateExtSceneButton = @".scene";
        public const string TemplateExtSliderSwitch = @".sliderswitch";
        public const string TemplateExtSwitch = @".switch";
        public const string TemplateExtValueDisplay = @".valuedisplay";
        public const string TemplateExtImageButton = @".imagebutton";
        public const string TemplateExtTimerButton = @".timerbutton";
        public const string TemplateExtDigitalAdjustment = @".digitaladjustment";
        public const string TemplateExtGroupBox = @".groupbox";
    }
}
