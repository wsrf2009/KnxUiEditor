using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Entity;

namespace UIEditor.SationUIControl
{
    class STGroupBox : STContainer
    {
        #region
        private GroupBoxNode node;
        #endregion

        public STGroupBox() { }

        public STGroupBox(GroupBoxNode node)
            : base(node)
        {
            this.node = node;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Region = new System.Drawing.Region(GetRoundRectangle(rect, this.node.Radius)); // 圆角矩形

            Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            if ((null == this.node.BackgroundImage) || (string.Empty == this.node.BackgroundImage))
            {
                if (UIEditor.Entity.ViewNode.EFlatStyle.Flat == this.node.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    g.FillRegion(brush, Region);
                }
            }
        }
    }
}
