using DemoMVVM.Model;
using DemoMVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMVVM.ViewModel
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PersonaViewModel PersonaSeleccionada { get; set; }

        private ObservableCollection<PersonaViewModel> persones;

		public ObservableCollection<PersonaViewModel> Persones
		{
			get { return persones; }
            set { persones = value; }
		}


        public MainPageViewModel()
        {
            GetListPersones();
            
        }

        public void NewPerson()
        {
            PersonaViewModel aux = new PersonaViewModel();
            aux.PersonaOriginal = new PersonaViewModel();
            PersonaSeleccionada = aux;

        }

        public void GetListPersones()
        {
            Persones = new ObservableCollection<PersonaViewModel>(Persona.GetPersones().Select(ele => new PersonaViewModel(ele)));
        }

    }
}
