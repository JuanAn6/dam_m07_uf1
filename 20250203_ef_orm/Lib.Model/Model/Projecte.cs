using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Model
{
    public class Projecte
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Empleat> Empleats { get; set; }
    }
}
