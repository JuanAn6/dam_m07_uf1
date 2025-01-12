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
    internal class MainPageViewModel : BaseVewModel
    {

        public PersonaViewModel PersonaSeleccionada { get; set; }

        private ObservableCollection<PersonaViewModel> persones;

		public ObservableCollection<PersonaViewModel> Persones
		{
			get { return persones; }
            set { persones = value; }
		}


        public MainPageViewModel()
        {
            Persones = new ObservableCollection<PersonaViewModel>(Persona.GetPersones().Select(ele => new PersonaViewModel(ele) ));
            
        }


    }
}
