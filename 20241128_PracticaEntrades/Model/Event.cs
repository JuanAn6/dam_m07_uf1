using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241128_PracticaEntrades.Model
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
        private Estat estat;
        private Sala sala;
        private List<Tarifa> tarifes;

        public Event(TipusEvent tipus, long id, string nom, string protagonista, string desc, string imgPath, DateTime data, Estat estat, Sala sala, List<Tarifa> tarifes)
        {
            this.tipus = tipus;
            this.id = id;
            this.nom = nom;
            this.protagonista = protagonista;
            this.desc = desc;
            this.imgPath = imgPath;
            this.data = data;
            this.estat = estat;
            this.sala = sala;
            this.tarifes = tarifes;
        }
    }
}
