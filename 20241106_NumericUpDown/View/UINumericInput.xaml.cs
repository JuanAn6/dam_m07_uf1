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

namespace _20241106_NumericUpDown.View
{
    public sealed partial class UINumericInput : UserControl
    {



        public int Valor
        {
            get { return (int)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(int), typeof(UINumericInput), new PropertyMetadata(0, ValorChangedCallbackStatic));

        //Modificador del valor desde fora i desde dins s'executa aixo!
        private static void ValorChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UINumericInput control = (UINumericInput)d;
            control.ValorChangedCallback(d,e);
        }

        private void ValorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Valor = Math.Clamp(Valor, Min, Max);
            showNumero();
        }

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(int), typeof(UINumericInput), new PropertyMetadata(100, MaxChangedCallbackStatic));

        private static void MaxChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UINumericInput control = (UINumericInput)d;
            control.MaxMinChangedCallback(d, e);
        }


        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(int), typeof(UINumericInput), new PropertyMetadata(0, MinChangedCallbackStatic));

        private static void MinChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UINumericInput control = (UINumericInput)d;
            control.MaxMinChangedCallback(d, e);
        }

        private void MaxMinChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Valor = Math.Clamp(Valor, Min, Max); //Math.Min(Max, Math.Min(Min, Valor));
        }

        public UINumericInput()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            showNumero();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            Valor++;
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            Valor--;
        }

        private void showNumero()
        {
            tbNum.Text = Valor + "";
        }

        private void tbNum_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            int res;
            if(int.TryParse(sender.Text, out res))
            {
                Valor = res;
            }
            showNumero();
        }

       
    }
}
