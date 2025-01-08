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

namespace DBDemo.View
{
    public sealed partial class PaginationControl : UserControl
    {
        public PaginationControl()
        {
            this.InitializeComponent();
        }


        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register(
            nameof(MinPage),
            typeof(int),
            typeof(PaginationControl),
            new PropertyMetadata(1));

        public static readonly DependencyProperty MaxPageProperty = DependencyProperty.Register(
            nameof(MaxPage),
            typeof(int),
            typeof(PaginationControl),
            new PropertyMetadata(1));

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
            nameof(CurrentPage),
            typeof(int),
            typeof(PaginationControl),
            new PropertyMetadata(1));

        public static readonly DependencyProperty IsPreviousPageButtonEnabledProperty = DependencyProperty.Register(
            nameof(IsPreviousPageButtonEnabled),
            typeof(bool),
            typeof(PaginationControl),
            new PropertyMetadata(true, (d, e) => (d as PaginationControl)?.UpdateButtons()));

        public static readonly DependencyProperty IsNextPageButtonEnabledProperty = DependencyProperty.Register(
            nameof(IsNextPageButtonEnabled),
            typeof(bool),
            typeof(PaginationControl),
            new PropertyMetadata(true, (d, e) => (d as PaginationControl)?.UpdateButtons()));

 

        public event TypedEventHandler<PaginationControl, EventArgs> PageChanged;

        public bool IsPreviousPageButtonEnabled
        {
            get => (bool)GetValue(IsPreviousPageButtonEnabledProperty);
            set => SetValue(IsPreviousPageButtonEnabledProperty, value);
        }

        public bool IsNextPageButtonEnabled
        {
            get => (bool)GetValue(IsNextPageButtonEnabledProperty);
            set => SetValue(IsNextPageButtonEnabledProperty, value);
        }

        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public int MinPage
        {
            get => (int)GetValue(PageCountProperty);
            set => SetValue(PageCountProperty, value);
        }

        public int MaxPage
        {
            get => (int)GetValue(MaxPageProperty);
            set => SetValue(MaxPageProperty, value);
        }

        private void UpdateButtons()
        {
            PreviousPageButton.IsEnabled = (CurrentPage > MinPage) && IsPreviousPageButtonEnabled;
            NextPageButton.IsEnabled = (CurrentPage < MaxPage) && IsNextPageButtonEnabled;
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = Math.Max(CurrentPage - 1, MinPage);
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = Math.Min(CurrentPage + 1, MaxPage);
        }

        private void CurrentPageNumberBox_ValueChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtons();
            PageChanged?.Invoke(
                this,
                new EventArgs());
        }

        /* private void CurrentPageNumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
         {
             UpdateButtons();
             PageChanged?.Invoke(
                 this,
                 new PaginationControlValueChangedEventArgs(
                     (int)args.OldValue,
                     (int)args.NewValue));
         }*/

    }

}
