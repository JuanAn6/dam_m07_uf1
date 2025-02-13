using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241128_PracticaEntrades.Model
{
    public class Tarifa : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string nom;
        private string desc;
        private decimal preu;
        private Event event_local;
        private Zona zona; // Si la zona es null es una tarifa general

    }
}
