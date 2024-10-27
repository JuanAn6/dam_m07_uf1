using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_CS.model
{
    internal class Persona
    {
        #region attributs
        
		private String nom;
        private int id;
        private String cognoms;

        #endregion

        public Persona(string nom, int id, string cognoms)
        {
            this.Nom = nom;
            this.Id = id;
            this.Cognoms = cognoms;
        }

        public String Nom
		{
			get { return nom; }
			set { 
				if (value == null || value.Length < 2) {
					throw new ArgumentException("Error en el nom");
				}
				nom = value; 
			}
		}

        public int Id { 
			get => id; 
			set => id = value; 
		}
        public string Cognoms {
			get => cognoms; 
			set => cognoms = value; 
		}

        public override bool Equals(object obj)
        {
            return obj is Persona persona &&
                   Id == persona.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id+". "+this.Nom+" "+this.Cognoms;
        }
    }
}
