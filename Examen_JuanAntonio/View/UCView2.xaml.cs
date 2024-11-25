using Examen_JuanAntonio.Model;
using System;
using System.Collections.Generic;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Examen_JuanAntonio.View
{
    public sealed partial class UCView2 : UserControl
    {

        public event EventHandler Delete;

        public UCView2()
        {
            this.InitializeComponent();
        }
        public Person ThePerson
        {
            get { return (Person)GetValue(ThePersonProperty); }
            set { SetValue(ThePersonProperty, value); }
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThePersonProperty =
            DependencyProperty.Register("ThePerson", typeof(Person), typeof(UCView2), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, EventArgs.Empty);
            Debug.WriteLine("Try delete uc");
        }
    }
}
