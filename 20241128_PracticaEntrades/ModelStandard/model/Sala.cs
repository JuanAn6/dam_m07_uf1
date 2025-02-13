using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241128_PracticaEntrades.Model
{
    public class Sala : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private long id;
        private string nom;
        private string adreca;
        private string municipi;

        private List<Zona> zones;

        private int numColumnes, numFiles; //MAX ? 
        private bool teMapa;

        public Sala(long id, string nom, string adreca, string municipi, List<Zona> zones, int numColumnes, int numFiles, bool teMapa)
        {
            this.Id = id;
            this.Nom = nom;
            this.Adreca = adreca;
            this.Municipi = municipi;
            this.Zones = zones;
            this.NumColumnes = numColumnes;
            this.NumFiles = numFiles;
            this.TeMapa = teMapa;

        }

        public long Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adreca { get => adreca; set => adreca = value; }
        public string Municipi { get => municipi; set => municipi = value; }
        public int NumColumnes { get => numColumnes; set => numColumnes = value; }
        public int NumFiles { get => numFiles; set => numFiles = value; }
        public bool TeMapa { get => teMapa; set => teMapa = value; }
        internal List<Zona> Zones { get => zones; set => zones = value; }

        public Sala(string nom, string adreca, string municipi, int numColumnes, int numFiles, bool teMapa, List<Zona> zones)
        {
            Nom = nom;
            Adreca = adreca;
            Municipi = municipi;
            NumColumnes = numColumnes;
            NumFiles = numFiles;
            TeMapa = teMapa;
            Zones = zones;
        }


        public override string ToString()
        {
            return $"Sala [Id={Id}, Nom={Nom}, Adreca={Adreca}, Municipi={Municipi}, " +
                   $"NumColumnes={NumColumnes}, NumFiles={NumFiles}, TeMapa={TeMapa}, " +
                   $"Zones=[{string.Join(", ", Zones)}]]";
        }
        
    }
}
