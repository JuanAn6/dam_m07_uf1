using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241010_InventoryManager.models
{
    internal class ItemInventory : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Item item;
		private int quantity;

        public ItemInventory(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public int Quantity
        {
			get { return quantity; }
			set { quantity = value; }
		}

        public String QuantityXAML
        {
            get { 
                return quantity == 0 ? "" : ""+quantity;
            }
        }

		public Item Item
		{
			get { return item; }
			set { item = value; }
		}

        public override bool Equals(object obj)
        {
            return obj is ItemInventory inventory && EqualityComparer<Item>.Default.Equals(item, inventory.item);
        }

        public override int GetHashCode()
        {
            int hashCode = 656306508;
            hashCode = hashCode * -1521134295 + EqualityComparer<Item>.Default.GetHashCode(item);
            hashCode = hashCode * -1521134295 + quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Item>.Default.GetHashCode(Item);
            return hashCode;
        }
    }
}
