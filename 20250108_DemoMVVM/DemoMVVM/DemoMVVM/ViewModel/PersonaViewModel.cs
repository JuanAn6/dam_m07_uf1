using DemoMVVM.Model;
using DemoMVVM.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMVVM.View
{
    public class PersonaViewModel : BaseVewModel
    {
        
        private Persona persona;

        
        public String Nom { get; set; }
        public SexeEnum Sexe { get; set; }
        public bool Actiu { get; set; }
        public String ImageURL { get; set; }
        public int Edat { get; set; }


        public PersonaViewModel(Persona per)
        {
            this.persona = per;
            this.Nom = per.Nom;
            this.Sexe = per.Sexe;
            this.Actiu = per.Actiu;
            this.ImageURL = per.ImageURL;
            this.Edat = per.Edat;

            
        
        }

        private static ArrayList listSexe;
        public static ArrayList ListSexe { 
            get {
                if (listSexe == null) listSexe = new ArrayList(Enum.GetValues(typeof(SexeEnum)));
                return listSexe;
            } 
        }

    }
}
