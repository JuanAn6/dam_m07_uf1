using _20241128_PracticaEntrades.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace _20241128_PracticaEntrades
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class SalasPage : Page
    {



        private List<Sala> Sales
        {
            get { return (List<Sala>)GetValue(SalesProperty); }
            set { SetValue(SalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SalesProperty =
            DependencyProperty.Register("Sales", typeof(List<Sala>), typeof(SalasPage), new PropertyMetadata(new List<Sala>()));


        public SalasPage()
        {
            this.InitializeComponent();

            Sales.Clear();

            Sales.Add(new Sala(1, "La Sala", "Cal font", "Igualada", new List<Zona>() , 10, 10, true));
            Sales.Add(new Sala(2, "Douwntown", "Zona uni", "Barcelona", new List<Zona>() , 20, 20, true));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //dgEvents.ItemsSource = Events;
            this.DataContext = this;
        }
    }
}
