using Examen_JuanAntonio.Model;
using Examen_JuanAntonio.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Examen_JuanAntonio
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


        ObservableCollection<Pair> couples = new ObservableCollection<Pair>();
        ObservableCollection<Person> personsSelect = new ObservableCollection<Person>();
        ObservableCollection<Person> persons = new ObservableCollection<Person>();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> list = new List<string> { "", "18-20", "20-29", "30-39", "40-49", "50-59" };

            cbAge.ItemsSource = list;
            persons = Person.GetPeople();

            

            gvPersons.ItemsSource = persons;
            gvCouples.ItemsSource= couples;
        }

        private void gvPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Person pSelected = (Person)gvPersons.SelectedItem;
            if (pSelected != null)
            {
                personsSelect.Clear();
                foreach (Person p in persons)
                {
                    if (
                        !p.Name.Equals(pSelected.Name) &&
                        (
                            (p.Preference == pSelected.Gender || p.Preference == Gender.Other) &&

                            (pSelected.Preference == p.Gender || pSelected.Preference == Gender.Other)
                        )

                    )
                    {
                        personsSelect.Add(p);
                    }
                }
                gvPersonSelect.ItemsSource = personsSelect;
            }
        }

        private void Button_Click_Make_Couple(object sender, RoutedEventArgs e)
        {
            Person p1 = (Person)gvPersons.SelectedItem;
            Person p2 = (Person)gvPersonSelect.SelectedItem;

            if(p1 != null && p2 != null )
            {
               

                Pair pair = new Pair(p1, p2);
                couples.Add(pair);

                int index = persons.IndexOf(p1);
                persons.RemoveAt(index);

                int index2 = persons.IndexOf(p2);
                persons.RemoveAt(index2);

                personsSelect.Clear();

            }
        }

        private void Button_Click_Separe_Couple(object sender, RoutedEventArgs e)
        {
            int i = (int)gvCouples.SelectedIndex;
            
            if (i > -1)
            {
               

                Pair p = (Pair)gvCouples.SelectedItem;

                Person p1 = p.PersonA;
                Person p2 = p.PersonB;

                p1.OldCouples.Add(p2);
                p2.OldCouples.Add(p1);

                persons.Add(p1);
                persons.Add(p2);

                couples.RemoveAt(i);

                personsSelect.Clear();
            }
        }

        private void cbAge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = (int)cbAge.SelectedIndex;

            if (i == -1) return;

            switch (i)
            {
                case 0://"":
                    persons = Person.GetPeople();
                    gvPersons.ItemsSource = persons;
                    break;
                case 1:// "18-20":
                    FiltrarPersons(18, 20);
                    break;
                case 2:// "20-29":
                    FiltrarPersons(20, 29);
                    break;
                case 3://"30-39":
                    FiltrarPersons(30, 39);
                    break;
                case 4://"40-49":
                    FiltrarPersons(40, 49);
                    break;
                case 5:// "50-59":
                    FiltrarPersons(50, 59);
                    break;
            }
        }

        private void FiltrarPersons(int min, int max)
        {
            ObservableCollection<Person> filter = new ObservableCollection<Person>();
            persons = Person.GetPeople();
            foreach (Person p in persons)
            {
                if (p.Age <= max && p.Age >= min)
                {
                    filter.Add(p);
                }
            }
            Debug.WriteLine(filter.Count);
            gvPersons.ItemsSource = filter;

        }

        private void UCView2_Delete(object sender, EventArgs e)
        {
            Debug.WriteLine("Try delete");
            UCView2 view = (UCView2)sender;
            Person p = (Person)view.ThePerson;
            int index = persons.IndexOf(p);
            persons.RemoveAt(index);
        }
    }
}
