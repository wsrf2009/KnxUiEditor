using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.Entity;

namespace UIEditor.Drawing
{
    public class Ruler : Panel
    {
        public enum Direction
        {
            /// <summary>
            /// 水平方向
            /// </summary>
            Horizental,
            /// <summary>
            /// 垂直方向
            /// </summary>
            Vertical
        }

        /// <summary>
        /// 标尺方向
        /// </summary>
        public Direction RuleDirection { get; set; }
        /// <summary>
        /// 标尺有效刻度长度
        /// </summary>
        public int ScaleWidth { get; set; }
        /// <summary>
        /// 标尺有效刻度高度
        /// </summary>
        public int ScaleHeight { get; set; }
        /// <summary>
        /// 标尺有效刻度偏移量
        /// </summary>
        public int ScaleOffset { get; set; }
        /// <summary>
        /// 标尺刻度的颜色
        /// </summary>
        public Color ScaleColor { get; set; }
        public List<Line> Projections { get; set; }
        private float Ratio { get; set; }

        public Ruler(float ratio)
        {
            this.Ratio = ratio;

            //减少闪烁  
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.Projections = new List<Line>();
        }

        /// <summary>
        /// 绘制标尺
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Direction.Horizental == this.RuleDirection)
            {
                this.Width = this.ScaleWidth + 2 * ScaleOffset;
                this.Height = this.ScaleHeight;
            }
            else
            {
                this.Width = this.ScaleWidth;
                this.Height = this.ScaleHeight + 2 * ScaleOffset;
            }

            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height); // 填充标尺背景色

            if (Direction.Horizental == this.RuleDirection)
            {
                var halfUnitHeight = this.Height - 17;
                var unitHeight = this.Height - 14;
                var packUnitHeight = this.Height - 4;

                e.Graphics.DrawLine(new Pen(this.ScaleColor), new Point(0, this.Height), new Point(this.Right, this.Height)); // 绘制标尺下边沿

                for (int i = 0; i <= this.ScaleWidth; i++)
                {
                    int offset = i + this.ScaleOffset;
                    if ((i % 50 == 0) || (i == this.ScaleWidth))
                    {
                        Pen pen = new Pen(this.ScaleColor);
                        Brush brush = new SolidBrush(this.ScaleColor);
                        Font font = this.Font;
                        if (i == this.ScaleWidth)
                        {
                            pen = new Pen(Color.FromArgb(180, Color.Green));
                            brush = new SolidBrush(Color.FromArgb(180, Color.Green));
                            font = new Font("粗体", 10);
                        }
                        e.Graphics.DrawLine(pen, new Point(offset, this.Height), new Point(offset, this.Height - packUnitHeight));
                        double v = (double)i / 50;
                        v /= this.Ratio;
                        v = Math.Round(v, 1);
                        e.Graphics.DrawString(v.ToString(), font, brush, offset + 2, this.Height - (packUnitHeight + 2));
                    }
                    else
                    {
                        if (i % 25 == 0)
                        {
                            e.Graphics.DrawLine(new Pen(this.ScaleColor), offset, this.Height, offset, this.Height - unitHeight);
                        }
                        else if (i % 5 == 0)
                        {
                            e.Graphics.DrawLine(new Pen(this.ScaleColor), offset, this.Height, offset, this.Height - halfUnitHeight);
                        }
                    }
                }
            }
            else
            {
                var halfUnitWidth = 3;
                var unitWidth = 6;
                var packUnitWidth = 16;

                e.Graphics.DrawLine(new Pen(this.ScaleColor), new Point(this.Width, 0), new Point(this.Width, this.Bottom)); // 绘制标尺右边沿

                for (int i = 0; i <= this.ScaleHeight; i++)
                {
                    int offset = i + this.ScaleOffset;
                    if ((i % 50 == 0) || (i == this.ScaleHeight))
                    {
                        Pen pen = new Pen(this.ScaleColor);
                        Brush brush = new SolidBrush(this.ScaleColor);
                        Font font = this.Font;
                        if (i == this.ScaleHeight)
                        {
                            pen = new Pen(Color.FromArgb(180, Color.Green));
                            brush = new SolidBrush(Color.FromArgb(180, Color.Green));
                            font = new Font("粗体", 10);
                        }

                        e.Graphics.DrawLine(pen, new Point(this.Width, offset), new Point(this.Width - packUnitWidth, offset));
                        double v = (double)i / 50;
                        v /= this.Ratio;
                        v = Math.Round(v, 1);
                        e.Graphics.DrawString(v.ToString(), font, brush, 3, offset + 2);
                    }
                    else
                    {
                        if (i % 25 == 0)
                        {
                            e.Graphics.DrawLine(new Pen(this.ScaleColor), this.Width, offset, this.Width - unitWidth, offset);
                        }
                        else if (i % 5 == 0)
                        {
                            e.Graphics.DrawLine(new Pen(this.ScaleColor), this.Width, offset, this.Width - halfUnitWidth, offset);
                        }
                    }
                }
            }

            foreach (Line line in this.Projections)
            {
                if (Direction.Horizental == this.RuleDirection)
                {
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(200, ColorTranslator.FromHtml("#3399FF")), 3.0f),
                        line.Begin.X + this.ScaleOffset - 1, line.Begin.Y - 1,
                        line.End.X + this.ScaleOffset - 1, line.End.Y - 1);
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(200, ColorTranslator.FromHtml("#3399FF")), 3.0f),
                        line.Begin.X - 1, line.Begin.Y + this.ScaleOffset - 1,
                        line.End.X - 1, line.End.Y + this.ScaleOffset - 1);
                }
            }
        }

        public void ClearProjection()
        {
            this.Projections.Clear();
        }

        public void SetProjection(int x1, int y1, int x2, int y2)
        {
            if (Direction.Horizental == this.RuleDirection)
            {
                this.Projections.Add(new Line(new Point(x1, y1), new Point(x2, y2)));
            }
            else
            {
                this.Projections.Add(new Line(new Point(x1, y1), new Point(x2, y2)));
            }
        }

        public void Zoom(float ratio)
        {
            this.Ratio = ratio;

            this.Refresh();
        }
    }
}
