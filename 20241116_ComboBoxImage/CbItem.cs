using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace _20241116_ComboBoxImage
{
    internal class CbItem
    {
        public string Img { get; set; }
        public string Text { get; set; }

        public CbItem(string img, string text)
        {
            this.Img = img;
            this.Text = text;
        }

    }
}
