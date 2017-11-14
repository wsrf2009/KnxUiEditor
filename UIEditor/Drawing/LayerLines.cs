using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Drawing
{
    public class LayerLines:Panel
    {
        internal enum EnumOperations
        {
            Operation_DrawLines,
            Operation_DrawRectangle
        };

        #region 变量
        private EnumOperations Operation;
        private List<Line> Lines;
        private Rectangle Rect;
        #endregion

        #region 构造函数
        public LayerLines(/*int width, int height*/)
        {
            this.BackColor = Color.Transparent;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        #region 覆写函数
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (EnumOperations.Operation_DrawLines == this.Operation)
            {
                if ((null != Lines) && (this.Lines.Count > 0))
                {
                    Pen pen = new Pen(Color.DodgerBlue, 1.8f);

                    foreach (Line line in this.Lines)
                    {
                        g.DrawLine(pen, line.Begin, line.End);
                    }
                }
            }
            else if (EnumOperations.Operation_DrawRectangle == this.Operation)
            {
                Pen pen = new Pen(Color.LightGray, 1.0f);
                pen.DashStyle = DashStyle.Dot;
                g.DrawRectangle(pen, this.Rect);
            }
        }
        #endregion

        #region 公共方法
        public void DrawLines(List<Line> lines)
        {
            this.Operation = EnumOperations.Operation_DrawLines;
            this.Lines = lines;
            this.Refresh();
        }

        public void DrawRectangleLightGrayDot(Rectangle rect)
        {
            this.Operation = EnumOperations.Operation_DrawRectangle;
            this.Rect = rect;
            this.Refresh();
        }

        public void SetNewSize(int width, int height)
        {
            this.Size = this.MaximumSize = this.MinimumSize = new Size(width, height);
        }
        #endregion
    }
}
