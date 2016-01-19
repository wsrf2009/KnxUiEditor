
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using SourceGrid;
using Structure;
using UIEditor.Component;
using Button = SourceGrid.Cells.Button;

namespace UIEditor.Entity
{
    /// <summary>
    /// 所有树上面添加元素的基础类，主要分配ID
    /// </summary>
    [Serializable]
    public abstract class ViewNode : TreeNode, ISerializable
    {
        public static int InitId = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds);
        public static int GenId()
        {
            return InitId++;
        }

        private int _id;

        #region 属性

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        #endregion

        #region 构造函数
        public ViewNode()
        {
            Id = GenId();
            this.Text = "ViewNode";
        }

        public ViewNode(KNXView knx)
        {
            _id = knx.Id;
            this.Text = knx.Text;
        }

        protected ViewNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        #region 转换函数
        protected void ToKnx(KNXView knx)
        {
            knx.Id = this.Id;
            knx.Text = this.Text;
        }

        #endregion

        #region 属性显示抽象函数

        public virtual void DisplayProperties(Grid grid)
        {

        }

        public virtual void ChangePropValues(CellContext context)
        {

        }

        #endregion

        #region 事件处理函数

        protected void PickImage(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.FileName = "";
                dlg.Multiselect = false;
                //dlg.InitialDirectory = MyCache.DefaultKnxResurceFolder;
                dlg.Filter = MyConst.PicFilter;
                dlg.FilterIndex = 3;
                dlg.RestoreDirectory = true;
                dlg.ValidateNames = true;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.SafeFileName != null)
                    {
                        string selectedFile = dlg.SafeFileName;
                        string imageFile = Path.Combine(MyCache.ProjImagePath, selectedFile);

                        if (File.Exists(imageFile))
                        {
                            // 使用现有资源图片
                            var myimage = Image.FromFile(imageFile);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.Resize(myimage, new Size(16, 16));
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = Path.GetFileName(imageFile);
                        }
                        else
                        {
                            // 复制图片文件到资源目录
                            File.Copy(dlg.FileName, Path.Combine(Application.StartupPath, imageFile));
                            var myimage = Image.FromFile(imageFile);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.Resize(myimage, new Size(16, 16));
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = Path.GetFileName(imageFile);
                        }
                    }
                }
            }
        }

        protected void PickColor(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var selectColor = ColorTranslator.FromHtml("#FF000000");

            var tempValue = btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value;
            if (tempValue != null)
            {
                selectColor = FrmMainHelp.HexStrToColor(MyConst.ValueColumn.ToString());
            }
            var myDialog = new ColorDialog { AllowFullOpen = false, ShowHelp = true, Color = selectColor };

            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = FrmMainHelp.ColorToHexStr(myDialog.Color);
            }
        }

        protected void ShowSaveEntityMsg(string knxtype)
        {
            MessageBox.Show(string.Format("保存{0}值出错！", knxtype), "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        public abstract ViewNode Clon2();
    }


}