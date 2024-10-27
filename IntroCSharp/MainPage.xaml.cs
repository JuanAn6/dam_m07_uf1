using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

namespace IntroCSharp
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //DOCUMENTACION ==== "MSDN funcion..."

            txbSortida.Text = "Hola";
            Debug.WriteLine(txbSortida.Text);

            int i = 0;
            float f = 234.5f;
            decimal nomina = 234.56M;
            Debug.WriteLine(i.ToString());
            Debug.WriteLine(f.ToString());
            Debug.WriteLine(nomina.ToString());

            String nom = "Juan";
            nom = nom + " Antonio";

            String presentacio = $"Hola como estas {nom}";
            
            txbSortida.Text = presentacio;

            Debug.WriteLine(presentacio);

            String select = @"Hola que tal 
                                como estamos
                                todo esta bien";

            Debug.WriteLine(select);

            String ip = "10.121.11.30";
            String [] valors = ip.Split(".");
            
            foreach( String valor in valors)
            {
                Debug.WriteLine(valor.PadLeft(3), "0");
            }


            float num = 234.345f;
            CultureInfo us = new CultureInfo("en-US");
            txbSortida.Text = num.ToString("#########.0000", us);

            decimal result = 0;

            
            if(Decimal.TryParse("1234,72", out result)){
                txbSortida.Text = result.ToString();
            }
            else
            {
                Debug.WriteLine("Numero malament");
            }

            //Dates

            DateTime now = DateTime.Now;
            txbSortida.Text = now.ToString();

            DateTime today = DateTime.Today; //No te en compte hores minuts segons 00:00:00
            txbSortida.Text = today.ToString();

            DateTime oneDate = new DateTime(2025, 09, 12, 4, 56, 23);
            txbSortida.Text = oneDate.ToString();


            txbSortida.Text = "  "+now.ToString("dd MMM yyyy HH:mm:ss");
            txbSortida.Text += "  "+now.ToString("dd/MM/yyyy");
            txbSortida.Text += "  "+now.ToString("dd MMM yy HH:mm:ss");


            string dateString = now.ToString("dd/MM/yyyy");

            DateTime newDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);

            txbSortida.Text += "  " + newDate.ToString();
        }
    }
}
