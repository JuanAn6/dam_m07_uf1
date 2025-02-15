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
    public class ZonaView : Zona, INotifyPropertyChanged 
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ZonaView(Zona z) : base(z.Desc, z.Nom, z.Numero, z.Capacitat, z.Color)
        {
        }

        public SolidColorBrush SolidColor{
            //ChatGPTeada Historica
            get {
                String c_s = base.Color;
                Windows.UI.Color c = ColorHelper.FromArgb(
                            255, // Opacidad (Alpha)
                            Convert.ToByte(c_s.Substring(2, 2), 16), // Rojo
                            Convert.ToByte(c_s.Substring(4, 2), 16), // Verde
                            Convert.ToByte(c_s.Substring(6, 2), 16)  // Azul
                        ); ;
                return new SolidColorBrush(c); 
            }
        }
      
    }
}
