using _20241128_PracticaEntrades.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class EventsPage : Page
    {
        //private ObservableCollection<Event> events = new ObservableCollection<Event>();



        private List<Event> Events
        {
            get { return (List<Event>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(List<Event>), typeof(EventsPage), new PropertyMetadata(new List<Event>()));




        public EventsPage()
        {
            this.InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            Events.Add(new Event(TipusEvent.TEATRE, 1, "La bella y la bestia ", "Adam Shadler", "Una tia y un toro", "ms-appx:///Assets/mustang_toro_tia.png", new DateTime(), Estat.NOU, null, null));

            this.DataContext = this;
        }
    }
}
