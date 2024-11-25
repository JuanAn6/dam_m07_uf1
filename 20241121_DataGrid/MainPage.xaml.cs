using _20241121_DataGrid.Model;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _20241121_DataGrid
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }



        public List<Hero> Heroes
        {
            get { return (List<Hero>)GetValue(HeroesProperty); }
            set { SetValue(HeroesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Heroes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeroesProperty =
            DependencyProperty.Register("Heroes", typeof(List<Hero>), typeof(MainPage), new PropertyMetadata(new List<Hero>()));



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Heroes = Hero.GetListOfHeroes();
            //dg.ItemsSource = Heroes;

            this.DataContext = this;
        }


    }
}
