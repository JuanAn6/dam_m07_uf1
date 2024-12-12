using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.model
{
    public class Dept : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private int dept_no;

        public int Dept_no
        {
            get { return dept_no; }
            set { dept_no = value; }
        }

        private string dNom;

        public string DNom
        {
            get { return dNom; }
            set { dNom = value; }
        }

        private string loc;

        public string Loc
        {
            get { return loc; }
            set { loc = value; }
        }

        public Dept(int dept_no, string dNom, string loc)
        {
            Dept_no = dept_no;
            DNom = dNom;
            Loc = loc;
        }


    }
}
