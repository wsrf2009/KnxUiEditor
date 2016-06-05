using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure.Control
{
    public class KNXRadioGroup : KNXContainer
    {
        public List<KNXControlBase> Controls { get; set; }
    }
}
