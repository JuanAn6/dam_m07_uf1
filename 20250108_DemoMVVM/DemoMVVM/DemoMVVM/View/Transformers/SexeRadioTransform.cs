using DemoMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DemoMVVM.View.Transformers
{
    internal class SexeRadioTransform : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SexeEnum aux = SexeEnum.DONA; //Valor perque no estigui null
            switch(parameter)
            {
                case "0": aux = SexeEnum.DONA; break;
                case "1": aux = SexeEnum.HOME; break;
                case "2": aux = SexeEnum.NO_DEFINIT; break;
            }
        
            return (SexeEnum)value == aux;
        
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //Del entorn gràfic al codi

            SexeEnum aux = SexeEnum.DONA; //Valor perque no estigui null
            switch (parameter)
            {
                case "0": aux = SexeEnum.DONA; break;
                case "1": aux = SexeEnum.HOME; break;
                case "2": aux = SexeEnum.NO_DEFINIT; break;
            }

            if ((bool)value)
            {
                //Si el radio button es selecciona canviem en sexe
                return aux;
            }
            else
            {
                //Si el radio button es desselecciona no cal fer res
                return DependencyProperty.UnsetValue;
            }

        }
    }
}
