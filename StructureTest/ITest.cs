using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StructureTest
{
    public interface ITest
    {
        bool CanFly { get; set; }

        string this[int index] { get; set; }


        event Action ActionFly;
    }

}
