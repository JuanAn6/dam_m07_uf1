using Examen_JuanAntonio.Model;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Examen_JuanAntonio.View
{
    public sealed partial class UCCouples : UserControl
    {
        public UCCouples()
        {
            this.InitializeComponent();
        }



        public Pair TheCouple   
        {
            get { return (Pair)GetValue(TheCoupleProperty); }
            set { SetValue(TheCoupleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheCoupleProperty =
            DependencyProperty.Register("TheCouple", typeof(Pair), typeof(UCCouples), new PropertyMetadata(null));


    }
}
