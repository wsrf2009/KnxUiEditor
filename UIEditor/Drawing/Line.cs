using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UIEditor.Drawing
{
    public class Line
    {
        public Point Begin;
        public Point End;

        public Line(Point begin, Point end)
        {
            this.Begin = begin;
            this.End = end;
        }
    }
}
