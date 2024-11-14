using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241113_AppEntradas.Model
{
    internal class Sala
    {
        private long id;
        private string nom;
        private string adreca;
        private string municipi;

        private List<Zona> zones;

        private int numColumnes, numFiles;
        private bool teMapa;
    }
}
