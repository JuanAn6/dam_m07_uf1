
using _20241128_PracticaEntrades.Model;
using DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
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




        public string SearchFilter
        {
            get { return (string)GetValue(SearchFilterProperty); }
            set { SetValue(SearchFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchFilterProperty =
            DependencyProperty.Register("SearchFilter", typeof(string), typeof(EventsPage), new PropertyMetadata(""));


        private ObservableCollection<Event> Events
        {
            get { return (ObservableCollection<Event>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(ObservableCollection<Event>), typeof(EventsPage), new PropertyMetadata(new ObservableCollection<Event>()));


        int ItemsPerPage = 5;

        public EventsPage()
        {
            this.InitializeComponent();
            
            Events.Clear();

            //Events.Add(new Event(TipusEvent.TEATRE, 1, "La bella y la bestia ", "Adam Shadler", "Una tia y un toro", "ms-appx:///Assets/mustang_toro_tia.png", DateTime.Now, Estat.NOU, null, null));
            //Events.Add(new Event(TipusEvent.TEATRE, 2, "Mamma Mia!", "La mama", "Pues mamma mia de toda la vida", "https://img.ecartelera.com/noticias/74700/74711-h3.jpg", DateTime.Now, Estat.NOU, null, null));

            LoadEventsList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            //dgEvents.ItemsSource = Events;
            this.DataContext = this;
        }

        private void pgc_PageChanged(UserControls.PaginationControl sender, EventArgs args)
        {
            LoadEventsList();
        }

        private void LoadEventsList()
        {
            
            Events.Clear();

            long num_events= 0;
            if (SearchFilter.Equals(""))
            {
                num_events = EventDB.CountEvents();
            }
            else
            {
                num_events = EventDB.CountEventsFiltered(SearchFilter);
            }

            int numPage = (int)MathF.Ceiling(num_events / (float)ItemsPerPage);

            pgc.MaxPage = numPage;
            pgc.MinPage = 1;

            pgc.CurrentPage = Math.Min(pgc.CurrentPage, numPage);


            List<Event> events = null;

            if (SearchFilter.Equals(""))
            {
                events = EventDB.GetEventsPage(pgc.CurrentPage, ItemsPerPage);
            }
            else
            {
                events = EventDB.GetEventsPageFiltered(pgc.CurrentPage, ItemsPerPage, SearchFilter);
            }



            foreach (Event e in events)
            {
                Events.Add(e);
            }



        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            SearchFilter = tb_search.Text;
            LoadEventsList();
        }
    }
}
