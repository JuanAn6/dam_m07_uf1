using _20241021_BuscaMinas.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using static _20241021_BuscaMinas.View.UCBoard;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _20241021_BuscaMinas
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
        }

        private void imgRestart_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UCBoard b = (UCBoard)board;
            b.Mode = MODE.READY;
            imgRestart.Source = new BitmapImage(new Uri("ms-appx:///Assets/img/face.png"));

        }

        private void board_OnGameOver(object sender, EventArgs e)
        {
            imgRestart.Source = new BitmapImage(new Uri("ms-appx:///Assets/img/face_ko.png"));
        }

        private async void board_OnGameWin(object sender, EventArgs e)
        {
            imgRestart.Source = new BitmapImage(new Uri("ms-appx:///Assets/img/face_win.gif"));

            MessageDialog dialog = new MessageDialog("WIN!");
            await dialog.ShowAsync();
        }
    }
}
