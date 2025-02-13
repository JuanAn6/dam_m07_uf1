using Lib.EF.Service;
using Lib.Model.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        EmpresaDBContext context;



        //public Empleat EmpleatSelected { get; set; }

        public List<Departament> listDepartaments = new List<Departament>();


        




        public Empleat EmpleatSelected
        {
            get { return (Empleat)GetValue(EmpleatSelectedProperty); }
            set { SetValue(EmpleatSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmpleatSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmpleatSelectedProperty =
            DependencyProperty.Register("EmpleatSelected", typeof(Empleat), typeof(MainWindow), new PropertyMetadata(null));





        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ContextFactory fabrica = new ContextFactory();
            context = fabrica.CreateDbContext();

            //initDades();


            dtgEmpleats.ItemsSource = context.Empleats.Include(e => e.Departament).Include( e => e.Projectes).ToList();


            //dtgDepartaments.ItemsSource = context.Departaments.ToList();
            //Departament info = context.Departaments.Include( d => d.Empleats ).Single(d => d.Nom.Equals("Informatica"));
            //dtgDepartaments.ItemsSource = info.Empleats.ToList();

            listDepartaments = context.Departaments.ToList();
            cbDept.ItemsSource = listDepartaments;

        }

        private void initDades()
        {
            Departament dInfo = new Departament() { Nom = "Informatica", Localitat = "Igualada" };
            Departament dVentas = new Departament() { Nom = "Ventas", Localitat = "Igualada" };

            context.Departaments.Add(dInfo);
            context.Departaments.Add(dVentas);


            context.Empleats.Add(new Empleat() { Nom = "Juan", DataNaix = DateTime.Now, Salary = 1000, Departament = dInfo });
            context.Empleats.Add(new Empleat() { Nom = "Alba", DataNaix = DateTime.Now, Salary = 2300, Departament = dInfo });
            context.Empleats.Add(new Empleat() { Nom = "Dico", DataNaix = DateTime.Now, Salary = 2700, Departament = dVentas });

            context.SaveChanges();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            showMessage("Successfull!");
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            context.ChangeTracker.Clear(); //Natejar contexte sense eliminarlo

            Empleat old = context.Empleats.Include(e => e.Departament ).Single( e => e.Id == EmpleatSelected.Id);

            EmpleatSelected.Nom = old.Nom;
            EmpleatSelected.Salary = old.Salary;
            EmpleatSelected.DataNaix = old.DataNaix;
            EmpleatSelected.Departament = old.Departament;



        }



        private async void showMessage(string message)
        {
            label_info.Content= message;
            await Task.Delay(1000);
            label_info.Content = "";
        }
    }
}