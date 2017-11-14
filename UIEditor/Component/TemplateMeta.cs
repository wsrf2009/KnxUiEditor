using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIEditor.Component
{
    public class TemplateMeta
    {
        public List<KNXView> Views { get; set; }

        public TemplateMeta()
        {
            this.Views = new List<KNXView>();
        }
    }
}
