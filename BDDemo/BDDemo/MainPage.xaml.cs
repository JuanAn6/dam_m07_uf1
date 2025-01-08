using DB;
using Model.model;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace BDDemo
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool modeInsert = false;

        private enum Mode
        {
            EDICIO,
            INSERCIO,
            INICIAL
        }

        private Mode mode;

        private Mode ModeActual
        {
            get { return mode; }
            set { 
                mode = value;
                switch (mode)
                {
                    case Mode.INSERCIO:
                        btnAdd.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        dataGrid.SelectedItem = null;
                        txbNum.Text = "";
                        txbNom.Text = "";
                        txbLoc.Text = "";
                        break;
                    case Mode.INICIAL:
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnAdd.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Collapsed;
                        dataGrid.SelectedItem = null;
                        txbNum.Text = "";
                        txbNom.Text = "";
                        txbLoc.Text = "";
                        break;
                    case Mode.EDICIO: 
                        btnAdd.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        public float Items_per_page { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ModeActual = Mode.INICIAL;
            LoadAll();
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem != null)
            {
                DeptDB.DeleteDept((dataGrid.SelectedItem as Dept).Dept_no);
                LoadAll();
            }
        }

        private void LoadAll()
        {
            long num_dept = DeptDB.CountDepts();
            
            countTB.Text = "" + num_dept;

            Items_per_page = 2;

            int numPage = (int)MathF.Ceiling(num_dept / (float)Items_per_page);
            
            pgc.MaxPage = numPage;
            pgc.MinPage = 1;

            pgc.CurrentPage = Math.Min(pgc.CurrentPage, numPage);

            List<Dept> departaments = DeptDB.GetDeptsPage(pgc.CurrentPage, Items_per_page);
            dataGrid.ItemsSource = departaments;

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dept d = (Dept) dataGrid.SelectedItem;
            if(d != null)
            {
                txbNum.Text = d.Dept_no.ToString();
                txbNom.Text = d.DNom.ToString();
                txbLoc.Text = d.Loc.ToString();

                ModeActual = Mode.EDICIO;
            }
            else
            {
                ModeActual = Mode.INICIAL;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (ModeActual == Mode.INSERCIO)
            {
                //Validacions...

                Dept d = new Dept(0, txbNom.Text, txbLoc.Text);

                DeptDB.insertDept(d);
                
                LoadAll();

                modeInsert = false;
            }
            else
            {
                Dept d = (Dept)dataGrid.SelectedItem;

                d.DNom = txbNom.Text;
                d.Loc = txbLoc.Text;

                if (d != null)
                {
                    //Validacions...

                    if (DeptDB.updateDept(d))
                    {


                    }
                }
            }

            ModeActual = Mode.INICIAL;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModeActual = Mode.INSERCIO;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ModeActual = Mode.INICIAL;
        }

        private void pgc_PageChanged(DBDemo.View.PaginationControl sender, EventArgs args)
        {
            LoadAll();
        }
    }
}
