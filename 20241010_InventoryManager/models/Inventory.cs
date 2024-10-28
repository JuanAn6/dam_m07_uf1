using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241010_InventoryManager.models
{
    internal class Inventory : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ItemInventory> inventoryList = new ObservableCollection<ItemInventory>();

        public Inventory(ObservableCollection<ItemInventory> inventoryList)
        {
            InventoryList = inventoryList;
        }

        public ObservableCollection<ItemInventory> InventoryList
        {
			get { return inventoryList; }
			set { inventoryList = value; }
		}



	}
}
