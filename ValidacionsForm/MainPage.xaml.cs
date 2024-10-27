using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ValidacionsForm
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private const string cross_icon = "";
        private const string check_icon = "";
        List<int> list_years = new List<int>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> list_days = new List<int>();

            for (int i = 0; i < 31; i++) {
                list_days.Add(i);
            }
            cbDia.ItemsSource = list_days;

            List<string> list_months = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                DateTime dt = new DateTime(2010,i,1);
                list_months.Add(dt.ToString("MMMM"));
            }
            cbMes.ItemsSource = list_months;


            
            DateTime now = DateTime.Today;

            for (int i = 0; i < 5; i++)
            {
                list_years.Add(now.Year);
                now = now.AddYears(1);
            }

            cbAny.ItemsSource = list_years;



            Debug.WriteLine("Main Loaded");
        }

        private void Change_tb_alcada(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ChangeText event");

            if(float.TryParse(tbAlcada.Text, out float value)){
                iconAlcada.Foreground = new SolidColorBrush(Colors.Green);
                iconAlcada.Text = check_icon;
                messageAlcada.Text = "Value ok";
            }
            else 
            {
                iconAlcada.Foreground = new SolidColorBrush(Colors.Red);
                iconAlcada.Text = cross_icon;
                messageAlcada.Text = "Value error";

            };


        }

        private void Change_tb_data_naix(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Change Date event");

            DateTime date;
            if (DateTime.TryParseExact(tbDataNaix.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None , out date))
            {
                iconDNaix.Foreground = new SolidColorBrush(Colors.Green);
                iconDNaix.Text = check_icon;
                messageDNaix.Text = "Value ok";


                cbDia.SelectedValue = date.Day;
                cbMes.SelectedValue = date.ToString("MMMM");
                cbAny.SelectedValue = date.Year;

            }
            else
            {
                iconDNaix.Foreground = new SolidColorBrush(Colors.Red);
                iconDNaix.Text = cross_icon;
                messageDNaix.Text = "Value error";

            };

        }

        [DllImport ("user32.dll",SetLastError =true)]
        static extern int MapVirtualKey(int uCode, uint uMapType);


        private void tbAlcada_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine("tecla pulsada");
            
            bool shift_press = Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            Debug.WriteLine("Shift: "+shift_press);

            char c = (char)MapVirtualKey((int)e.Key, (uint)2);
            e.Handled = !(
                e.Key == VirtualKey.Back || 
                (Char.IsNumber(c) && !shift_press) || 
                (c == ',' && !tbAlcada.Text.Contains(","))
            );

            //if (
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number0) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number1) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number2) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number3) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number4) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number5) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number6) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number7) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number8) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Number9) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad0) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad1) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad2) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad3) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad4) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad5) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad6) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad7) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad8) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.NumberPad9) ||
            //    !(e.OriginalKey == Windows.System.VirtualKey.Decimal)
            //)
            //{
            //    e.Handled = false;
            //}
        }

        private void tbDataNaix_TextChanged(object sender, KeyRoutedEventArgs e)
        {

            Debug.WriteLine("tecla pulsada");

            bool shift_press = Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            Debug.WriteLine("Shift: " + shift_press);

            char c = (char)MapVirtualKey((int)e.Key, (uint)2);
            e.Handled = !(
                e.Key == VirtualKey.Back ||
                (Char.IsNumber(c) && !shift_press) ||
                (c == '/' || (c == '7' && !shift_press)) && 
                !(tbAlcada.Text.Count(a => a == '/') == 2))
            );

        }
    }
}
