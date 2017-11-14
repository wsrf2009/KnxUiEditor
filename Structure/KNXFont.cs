using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Structure
{
    public class KNXFont
    {
        public string Color { get; set; }
        public int Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Strikeout { get; set; }
        public bool Underline { get; set; }

        public KNXFont() { }

        public KNXFont(Color c, int size, bool bold, bool italic, bool strikeout, bool underline)
        {
            this.Color = ColorHelper.ColorToHexStr(c);
            this.Size = size;
            this.Bold = bold;
            this.Italic = italic;
            this.Strikeout = strikeout;
            this.Underline = underline;
        }
    }
}
