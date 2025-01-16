using DemoMVVM.Model;
using DemoMVVM.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DemoMVVM.View
{
    public class PersonaViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Persona persona;

        public PersonaViewModel PersonaOriginal { get; set; }

        public int Id { get; set; }
        public String Nom { get; set; }
        public SexeEnum Sexe { get; set; }
        public bool Actiu { get; set; }
        public String ImageURL { get; set; }
        public String Edat { get; set; }

        public String EdatError
        {
            get
            {
                if (Persona.ValidaEdat(Edat))
                {
                    return "";
                }
                else
                {
                    return "Error en la edat de la persona!";
                }
            }
        }
        public Brush NameBorderBrush
        {
            get
            {
                if (Persona.ValidaNom(Nom))
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }
        public Brush EdatBackground
        {
            get
            {
                if (Persona.ValidaEdat(Edat))
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }

        public Visibility FormVisibility
        {
            get
            {
                return (this.PersonaOriginal != null) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility CanvelVisibility
        {
            get
            {

                if (this.PersonaOriginal == null ||
                    !this.Nom.Equals(this.PersonaOriginal.Nom) ||
                    !this.Edat.Equals(this.PersonaOriginal.Edat + "") ||
                    this.Actiu != this.PersonaOriginal.Actiu ||
                    this.Sexe != this.PersonaOriginal.Sexe

                )
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }

            }
        }

        public bool EsValida
        {
            get
            {
                return (CanvelVisibility == Visibility.Visible && (Persona.ValidaEdat(Edat) && Persona.ValidaNom(Nom)));
            }
        }


        public PersonaViewModel(PersonaViewModel per)
        {
            this.PersonaOriginal = per;
            this.persona = per.persona;
            this.Id = per.Id;
            this.Nom = per.Nom;
            this.Sexe = per.Sexe;
            this.Actiu = per.Actiu;
            this.ImageURL = per.ImageURL;
            this.Edat = per.Edat;



        }

        public PersonaViewModel(Persona per)
        {

            this.persona = per;
            this.Id = per.Id;
            this.Nom = per.Nom;
            this.Sexe = per.Sexe;
            this.Actiu = per.Actiu;
            this.ImageURL = per.ImageURL;
            this.Edat = per.Edat + "";

        }

        public PersonaViewModel()
        {
            this.Id = -1;
            this.Nom = "";
            this.Sexe = SexeEnum.NO_DEFINIT;
            this.Actiu = false;
            this.ImageURL = "https://picsum.photos/400/200";
            this.Edat = "";
        }

        private static ArrayList listSexe;



        public static ArrayList ListSexe
        {
            get
            {
                if (listSexe == null) listSexe = new ArrayList(Enum.GetValues(typeof(SexeEnum)));
                return listSexe;
            }
        }





        public void Save()
        {

            int edat_aux;
            if (int.TryParse(this.Edat, out edat_aux))
            {
                this.persona.Edat = edat_aux;

                PersonaOriginal.Edat = this.Edat;

                PersonaOriginal.Nom = this.persona.Nom = this.Nom;
                PersonaOriginal.Sexe = this.persona.Sexe = this.Sexe;
                PersonaOriginal.Actiu = this.persona.Actiu = this.Actiu;

            }


            //demanar a la UI que actualitzi el botó cancel o 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanvelVisibility"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EsValida"));

        }

        public void Cancel()
        {
            this.Edat = PersonaOriginal.Edat;
            this.Nom = PersonaOriginal.Nom;
            this.Sexe = PersonaOriginal.Sexe;
            this.Actiu = PersonaOriginal.Actiu;
        }
    }
}
