using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241010_InventoryManager.models
{
    internal class Recipe : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private String name;
        private ObservableCollection<KeyValuePair<Item, int>> items;
        private Item result;

        public Recipe(string nom, ObservableCollection<KeyValuePair<Item, int>> items, Item result)
        {
            this.Name = nom;
            this.Items = items;
            this.Result = result;
        }

        public Recipe(string nom, Item result)
        {
            this.Name = nom;
            this.Result = result;
            this.Items = new ObservableCollection<KeyValuePair<Item, int>>();
        }


        public Item Result
        {
            get { return result; }
            set { result = value; }
        }

        public ObservableCollection<KeyValuePair<Item, int>> Items
        {
            get { return items; }
            set { items = value; }
        }


        public String Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}