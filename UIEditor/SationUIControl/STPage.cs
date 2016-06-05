using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Entity;

namespace UIEditor.SationUIControl
{
    class STPage : STContainer
    {
        public STPage() { }

        public STPage(PageNode node)
            : base(node)
        {
            this.Location = new Point(0, 0);
            this.Size = this.MinimumSize = this.MaximumSize = new Size(node.Width, node.Height);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.AutoScroll = true;
        }
    }
}
