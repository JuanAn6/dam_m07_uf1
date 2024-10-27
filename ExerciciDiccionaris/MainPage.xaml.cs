using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ExerciciDiccionaris
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Dracula.txt"));
            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                int words = 0;
                List<String> words_readed = new List<String>();
                DateTime date = DateTime.Now;

                Dictionary<String, int> map = new Dictionary<String, int>();

                while (streamReader.Peek() >= 0)
                {
                    //Debug.WriteLine(string.Format("the line is {0}", streamReader.ReadLine()));

                    string line = streamReader.ReadLine();

                    Array arr_words = line.Split(new char[] { '\'','#','\"', ',', '.', ';', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries); ;
                    

                    //words += arr_words.Length;
                    
                    foreach (string word in arr_words)
                    {
                        if (!map.ContainsKey(word.ToLower()))
                        {
                            map.Add(word.ToLower(), 1);
                        }
                        else
                        {
                            map[word.ToLower()]++;
                        }


                        if (!words_readed.Contains(word.ToLower()))
                        {
                            //Debug.WriteLine(word.ToLower());
                            words_readed.Add(word.ToLower());
                        }
                    }

                }

                
                foreach(KeyValuePair<string, int> value in map){
                    Debug.WriteLine("Word: "+value.Key + ", Count: " + value.Value);
                }


                Debug.WriteLine("Total words: " + words_readed.Count);
                Debug.WriteLine("Total dictionari words: " + map.Count);
                
                DateTime finalDate = DateTime.Now;

                TimeSpan time = finalDate - date;

                Debug.WriteLine("Exec time: " + time.TotalMilliseconds);


            }
        }
    }
}
