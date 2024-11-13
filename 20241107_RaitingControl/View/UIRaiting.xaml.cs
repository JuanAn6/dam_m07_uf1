using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace _20241107_RaitingControl.View
{
    public sealed partial class UIRaiting : UserControl
    {

        private const int CAMELOS = 5;
        public UIRaiting()
        {
            this.InitializeComponent();
        }

        private bool loaded = false;

        public String Orientation
        {
            get { return (String)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(String), typeof(UIRaiting), new PropertyMetadata(0, OrientationChangedCallbackStatic));


        private static void OrientationChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIRaiting control = (UIRaiting)d;
            control.OrientationChangedCallback(d, e);
        }

        private void OrientationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIRaiting control = (UIRaiting)d;
            Debug.WriteLine("Orientation: " + control.Orientation);
            if(control.Orientation.Equals("Vertical")) 
            {
                stackPanel.Orientation = Windows.UI.Xaml.Controls.Orientation.Vertical;
            }
            else
            {
                stackPanel.Orientation = Windows.UI.Xaml.Controls.Orientation.Horizontal;
            }
        }

        public int Contador
        {
            get { return (int)GetValue(ContadorProperty); }
            set { SetValue(ContadorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContadorProperty =
            DependencyProperty.Register("Contador", typeof(int), typeof(UIRaiting), new PropertyMetadata(0, ContadorChangedCallbackStatic));


        private static void ContadorChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIRaiting control = (UIRaiting)d;
            control.ContadorChangedCallback(d, e);
        }

        private void ContadorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIRaiting control = (UIRaiting)d;
            tbContador.Text = control.Contador.ToString();
            CangeOpacityContador();
        }

        private void CangeOpacityContador()
        {
            if (loaded)
            {


                for (int i = 1; i <= CAMELOS; i++)
                {
                    if (i <= Contador)
                    {
                        stackPanel.Children[i - 1].Opacity = 1;
                    }
                    else
                    {
                        stackPanel.Children[i - 1].Opacity = 0.3;

                    }
                }

            }
        }

        private void UserControl_Loading(FrameworkElement sender, object args)
        {

            for(int i = 1; i <= CAMELOS; i++)
            {
                Image img = new Image();
                BitmapImage bi = new BitmapImage();
                bi.UriSource = new Uri("ms-appx://20241107_RaitingControl/Assets/camelo_nano_transparente.png");
                img.Source = bi;
                img.Height = 30;
                
                if(i >= Contador)
                {
                    img.Opacity = 0.3;
                }

                img.Tag = i;
                //Se añaden los eventos
                img.PointerMoved += Img_PointerMoved;
                img.PointerExited += Img_PointerExited;
                img.Tapped += Img_Tapped;

                stackPanel.Children.Add(img);
            }

            loaded = true;
            CangeOpacityContador();
        }

        private void Img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image img = (Image)sender;
            Contador = (int)img.Tag;
            
        }

        private void Img_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            ChangeOpacity(sender, 0.6);
        }
        private void Img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChangeOpacity(sender, 0.3);
        }

        private void ChangeOpacity(object sender, double opacity)
        {
            Image img = (Image)sender;

            for (int i = 1; i <= (int)img.Tag; i++)
            {
                if (stackPanel.Children[i - 1].Opacity < 1)
                {
                    stackPanel.Children[i - 1].Opacity = opacity;
                }

            }
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            Contador = 0;
        }
    }
}
