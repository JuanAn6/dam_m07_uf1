using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Model
{
    public class Departament : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }

        [StringLength(200)]
        public string Nom { get; set; }
        public string Localitat {  get; set; }
        public ICollection<Empleat> Empleats { get; set; }


        public override string? ToString()
        {
            return Nom+" - "+Localitat;
        }
    }
}
