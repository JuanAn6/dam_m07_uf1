using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace _20241021_BuscaMinas
{
    internal class Punt
    {
        public int row;
        public int col;

        public bool IsFlagged {  get; set; }

        public TextBlock MinaText { get; set; }

        public Punt(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

  
        public override bool Equals(object obj)
        {
            return obj is Punt punt &&
                   row == punt.row &&
                   col == punt.col;
        }

        public override int GetHashCode()
        {
            int hashCode = -1720622044;
            hashCode = hashCode * -1521134295 + row.GetHashCode();
            hashCode = hashCode * -1521134295 + col.GetHashCode();
            return hashCode;
        }
    }
}
