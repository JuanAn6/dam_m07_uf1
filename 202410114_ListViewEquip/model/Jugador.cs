using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202410114_ListViewEquip.model
{
    public class Jugador : Persona
    {
        private int dorsal;



        public Jugador(long id, string nom, string congnoms, string nacionalitat, string urlFoto, int dorsal) : base(id, nom, congnoms, nacionalitat, urlFoto)
        {
            Dorsal = dorsal;
        }

        public int Dorsal { get => dorsal; set => dorsal = value; }



    }
}
