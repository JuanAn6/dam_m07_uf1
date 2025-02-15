using _20241128_PracticaEntrades.Model;
using _20241128_PracticaEntrades.Views;
using DB;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Model.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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



        public string SearchFilter
        {
            get { return (string)GetValue(SearchFilterProperty); }
            set { SetValue(SearchFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchFilterProperty =
            DependencyProperty.Register("SearchFilter", typeof(string), typeof(SalasPage), new PropertyMetadata(""));



        private ObservableCollection<Sala> Sales
        {
            get { return (ObservableCollection<Sala>)GetValue(SalesProperty); }
            set { SetValue(SalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SalesProperty =
            DependencyProperty.Register("Sales", typeof(ObservableCollection<Sala>), typeof(SalasPage), new PropertyMetadata(new ObservableCollection<Sala>()));

        int ItemsPerPage = 10;
        public SalasPage()
        {
            this.InitializeComponent();

            LoadSalesList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //dgEvents.ItemsSource = Events;
            this.DataContext = this;
        }

        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            Frame frm = this.Parent as Frame;
            frm.Navigate(typeof(EdicioSala));
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {

            Sala s = dgSales.SelectedItem as Sala;
            if (s != null)
            {
                Frame frm = this.Parent as Frame;
                frm.Navigate(typeof(EdicioSala), s);
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(dgSales.SelectedItem != null)
            {
                Sala s = dgSales.SelectedItem as Sala;
                SalaDB.DeleteSala(s.Id);
                LoadSalesList();
            }
        }

        private void pgc_PageChanged(UserControls.PaginationControl sender, EventArgs args)
        {
            LoadSalesList();
        }

        

        private void LoadSalesList()
        {
            Sales.Clear();

            //Sales.Add(new Sala(1, "La Sala", "Cal font", "Igualada", new List<Zona>() , 10, 10, true));
            //Sales.Add(new Sala(2, "Downtown", "Zona uni", "Barcelona", new List<Zona>() , 20, 20, true));

            long num_sales = 0;
            if (SearchFilter.Equals(""))
            {
                num_sales = SalaDB.CountSalas();
            }
            else
            {
                num_sales = SalaDB.CountSalasFiltered(SearchFilter);
            }

            int numPage = (int)MathF.Ceiling(num_sales / (float)ItemsPerPage);

            pgc.MaxPage = numPage;
            pgc.MinPage = 1;

            pgc.CurrentPage = Math.Min(pgc.CurrentPage, numPage);

            
            List<Sala> sales = null;
            
            if (SearchFilter.Equals(""))
            {
                sales = SalaDB.GetSalasPage(pgc.CurrentPage, ItemsPerPage);
            }
            else
            {
                sales = SalaDB.GetSalasPageFiltered(pgc.CurrentPage, ItemsPerPage, SearchFilter);
            }


            foreach (Sala s in sales)
            {
                Sales.Add(s);
            }

        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            SearchFilter = tb_search.Text;
            LoadSalesList();
        }
    }
}
