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
            List<Dept> departaments = DeptDB.GetDepts();
            dataGrid.ItemsSource = departaments;
            countTB.Text = "" + DeptDB.CountDepts();
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

                Dept d = new Dept(Int32.Parse(txbNum.Text), txbNom.Text, txbLoc.Text);

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
    }
}
