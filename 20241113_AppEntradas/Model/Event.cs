using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241113_AppEntradas.Model
{
    internal class Event
    {
        private TipusEvent tipus;
        private long id;
        private string nom;
        private string protagonista;
        private string desc;
        private string imgPath;
        private DateTime data;

        private Sala sala;
        private List<Tarifa> tarifes;


    }
}
