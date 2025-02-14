using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241128_PracticaEntrades.Model
{
    public class Event : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TipusEvent tipus;
        private int id;
        private string nom;
        private string protagonista;
        private string desc;
        private string imgPath;
        private DateTime data;
        private Estat estat;
        private Sala sala;
        private List<Tarifa> tarifes;

        public Event(TipusEvent tipus, int id, string nom, string protagonista, string desc, string imgPath, DateTime data, Estat estat, Sala sala, List<Tarifa> tarifes)
        {
            this.tipus = tipus;
            this.id = id;
            this.nom = nom;
            this.protagonista = protagonista;
            this.desc = desc;
            this.imgPath = imgPath;
            this.data = data;
            this.estat = estat;
            this.sala = sala;
            this.tarifes = tarifes;
        }

        public TipusEvent Tipus { get => tipus; set => tipus = value; }
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Protagonista { get => protagonista; set => protagonista = value; }
        public string Desc { get => desc; set => desc = value; }
        public string ImgPath { get => imgPath; set => imgPath = value; }
        public DateTime Data { get => data; set => data = value; }
        public Estat Estat { get => estat; set => estat = value; }
        public Sala Sala { get => sala; set => sala = value; }
        public List<Tarifa> Tarifes { get => tarifes; set => tarifes = value; }

        public string FormatDate
        {
            get => Data.ToString("dd/MM/yyyy");
        }
    }
}
