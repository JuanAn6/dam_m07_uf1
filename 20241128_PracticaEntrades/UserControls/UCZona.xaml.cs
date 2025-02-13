using _20241128_PracticaEntrades.Model;
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

namespace _20241128_PracticaEntrades.UserControls
{
    public sealed partial class UCZona : UserControl
    {
        public UCZona()
        {
            this.InitializeComponent();
        }

        public ZonaView LaZona
        {
            get { return (ZonaView)GetValue(LaZonaProperty); }
            set { SetValue(LaZonaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LaZona.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaZonaProperty =
            DependencyProperty.Register("LaZona", typeof(ZonaView), typeof(UCZona), new PropertyMetadata(null));

    }
}
