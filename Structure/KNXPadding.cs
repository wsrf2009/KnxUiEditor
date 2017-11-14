using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Structure
{
    [Serializable]
    public class KNXPadding
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public KNXPadding()
        {

        }

        public KNXPadding(int l, int t, int r, int b)
        {
            this.Left = l;
            this.Top = t;
            this.Right = r;
            this.Bottom = b;
        }

        public KNXPadding(Padding padding)
        {
            this.Left = padding.Left;
            this.Top = padding.Top;
            this.Right = padding.Right;
            this.Bottom = padding.Bottom;
        }

        public Padding ToPadding()
        {
            return new Padding(this.Left, this.Top, this.Right, this.Bottom);
        }
    }
}
