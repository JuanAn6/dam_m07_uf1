using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241010_InventoryManager.models
{
    public class Item : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private String name;
        private String desc;
        private String url;
        private int id;


        public Item(int id, string name, string desc, string url)
        {
            this.Id = id;
            this.Name = name;
            this.Desc = desc;
            this.Url = url;
        }

        

        public String Name
        {
            get { return name; }
            set { 
                name = value;
                //Notificate of change
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public String Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public String Url
        {
            get { return url; }
            set { url = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public override String ToString()
        {
            return Name+" - "+Desc;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   name == item.name &&
                   desc == item.desc &&
                   url == item.url &&
                   id == item.id &&
                   Name == item.Name &&
                   Desc == item.Desc &&
                   Url == item.Url &&
                   Id == item.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -789212024;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(desc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(url);
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Desc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }
    }
}
