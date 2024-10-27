using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_CS.model
{
    internal class Client : Persona
    {
        private int clientCode;

        public int ClientCode
        {
            get { return clientCode; }
            set { clientCode = value; }
        }

        public Client(int clientCode, string nom, int id, string cognoms) : base(nom, id, cognoms)
        {
            this.ClientCode = clientCode;
        }
    }
}
