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
using Windows.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using _20241128_PracticaEntrades.Model;





// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace _20241128_PracticaEntrades.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EdicioSala : Page
    {
        public EdicioSala()
        {
            this.InitializeComponent();
            
        }

        public double CELL_SIZE = 20;

        public ObservableCollection<ZonaView> list_zones = new ObservableCollection<ZonaView>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                //recojer el parametro y hacer la query para popular todos lo campos
                //Crear un objeto para gestionar el formulario ?
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            //Preparació de la vista de columns i rows
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            for(int i = 1; i <= 18; i++) rows.Add(i);
            for(int i = 1; i <= 36; i++) columns.Add(i);
            tb_rows.ItemsSource = rows;
            tb_columns.ItemsSource = columns;

            tb_columns.SelectedIndex = 5;
            tb_rows.SelectedIndex = 5;


            RenderBoxSalaGrid();


            //Zones de exemple

            list_zones.Clear();
            list_zones.Add(new ZonaView(new Zona("Desc zona 1", "Zona 1", 1, 20, "#caa588")));
            list_zones.Add(new ZonaView(new Zona("Desc zona 2", "Zona 2", 2, 20, "#88ca95")));
            list_zones.Add(new ZonaView(new Zona("Desc zona 3", "Zona 3", 3, 20, "#88b9ca")));
            list_zones.Add(new ZonaView(new Zona("Desc zona 4", "Zona 4", 4, 20, "#a788ca")));

            tb_llista_zones.ItemsSource = list_zones;
        }


        private void RenderBoxSalaGrid()
        {
            //Renderitza la zona on es pot pintar
            grid_sala.Children.Clear();

            if (tb_rows.SelectedItem == null || tb_columns.SelectedItem == null) return;

            for (int row = 0; row < (int)tb_rows.SelectedItem; row++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(CELL_SIZE);
                grid_sala.RowDefinitions.Add(rowDefinition);
            }

            for (int col = 0; col < (int)tb_columns.SelectedItem; col++)
            {
                ColumnDefinition colDefinition = new ColumnDefinition();
                colDefinition.Width = new GridLength(CELL_SIZE);
                grid_sala.ColumnDefinitions.Add(colDefinition);
            }

            for (int row = 0; row < (int)tb_rows.SelectedItem; row++)
            {
                for(int col = 0; col < (int)tb_columns.SelectedItem; col++)
                {
                    //Rectangle square = new Rectangle
                    //{
                    //    Width = BOX_SIZE,
                    //    Height = BOX_SIZE,
                    //    Fill = new SolidColorBrush(Colors.Blue), // Azul
                    //    Stroke = new SolidColorBrush(Colors.Black), // Negro
                    //    StrokeThickness = 2
                    //};
                    //Canvas.SetTop(square, (MARGE + BOX_SIZE) * row);
                    //Canvas.SetLeft(square, (MARGE + BOX_SIZE) * col);

                    //canvas_sala.Children.Add(square);

                    StackPanel b = new StackPanel();
                    b.Tapped += B_Tapped;
                    b.Background = new SolidColorBrush(Colors.Blue);
                    b.Tag = new Point(row, col);
                    Grid.SetColumn(b, col);
                    Grid.SetRow(b, row);
                    grid_sala.Children.Add(b);
                    

                }
            }


        }

        private void B_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel b = sender as StackPanel;
            Color color = ((SolidColorBrush)OpenColorPickerButton.Background).Color;
            b.Background = new SolidColorBrush(color);

            if(tb_llista_zones.SelectedItem != null)
            {
                Point p = (Point)b.Tag;
                ((ZonaView)tb_llista_zones.SelectedItem).Cadires.Add(new Cadira((int)p.X, (int)p.Y));
            }


        }

        private void tb_columns_rows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RenderBoxSalaGrid();
        }

        private void CloseFlyout_Click(object sender, RoutedEventArgs e)
        {
            if (OpenColorPickerButton.Flyout is Flyout flyout)
            {
                flyout.Hide();
            }

        }


        private void CustomColorPicker_ColorChanged(Windows.UI.Xaml.Controls.ColorPicker sender, Windows.UI.Xaml.Controls.ColorChangedEventArgs args)
        {
            Color c = sender.Color;
            Debug.WriteLine(c.ToString());

            //OpenColorPickerButton.Background = new SolidColorBrush(c);
        }

        private void tb_llista_zones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenColorPickerButton.Background = ((ZonaView)tb_llista_zones.SelectedItem).SolidColor;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

            List<Zona> list = new List<Zona>();
            foreach(Zona z in list_zones)
            {
                list.Add(z);
                Debug.WriteLine("Zones: " + z.ToString());
            }

            Sala sala_save = new Sala(tb_nom.Text, tb_adreca.Text, tb_municipi.Text, (int)tb_columns.SelectedItem, (int)tb_rows.SelectedItem, true, list);

            Debug.WriteLine(sala_save.ToString());
        }
    }
}
