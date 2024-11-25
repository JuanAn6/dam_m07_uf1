using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_JuanAntonio.Model
{
    public class Pair : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Pair(Person personA, Person personB)
        {
            PersonA = personA;
            PersonB = personB;
        }

        public Person PersonA { get; set; }
        public Person PersonB { get; set; }
    }
}
