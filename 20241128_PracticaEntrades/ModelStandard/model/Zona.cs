using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace _20241128_PracticaEntrades.Model
{
    public class Zona : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string desc;
        private string nom;
        private int numero;
        private int capacitat;
        private string color; //Color en formato #FFFFFF
        private List<Cadira> cadires;

        public List<Cadira> Cadires
        {
            get { return cadires; }
            set { cadires = value; }
        }
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public int Capacitat
        {
            get { return capacitat; }
            set { capacitat = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public Zona(string desc, string nom, int numero, int capacitat, string color)
        {
            Desc = desc;
            Nom = nom;
            Numero = numero;
            Capacitat = capacitat;
            Color = color;
            Cadires = new List<Cadira>();
        }

        public override string ToString()
        {
            string cad = "";

            cad += nom;
            cad += " - "; 
            cad += numero;
            cad += " - ";
            cad += capacitat;
            cad += " - "; 
            cad += color;
            cad += " - "; 
            cad += desc;
            cad += " - ";
            cad += " Cadires: ";

            foreach (Cadira cadira in Cadires)
            {
                cad += "    "+cadira.ToString();
            }

            return cad;
        }
    }
}
