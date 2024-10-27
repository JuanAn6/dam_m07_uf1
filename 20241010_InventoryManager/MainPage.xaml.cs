using _20241010_InventoryManager.models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _20241010_InventoryManager
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Item> items = new ObservableCollection<Item>();

        ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

        Inventory inventoryStack = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           

            items.Add(new Item(0, "Axe", "Wooden bar with end of rock", "https://picsum.photos/id/1/100/100"));
            items.Add(new Item(1, "Torch", "Wooden with fire", "https://picsum.photos/id/2/100/100"));
            items.Add(new Item(2, "Cloth", "Peace of cloth", "https://picsum.photos/id/3/100/100"));
            items.Add(new Item(3, "Wood", "Literaly wood", "https://picsum.photos/id/4/100/100"));
            items.Add(new Item(4, "Iron", "Literaly iron", "https://picsum.photos/id/5/100/100"));
            items.Add(new Item(5, "Coal", "Literaly coal", "https://picsum.photos/id/6/100/100"));

            lsvItems.ItemsSource = items;


            recipes.Add(new Recipe("Torch", new ObservableCollection<KeyValuePair<Item, int>> {
                new KeyValuePair<Item, int>(items.ElementAt(5), 1),
                new KeyValuePair<Item, int>(items.ElementAt(3), 1)
                },
                items.ElementAt(1))
            );
            recipes.Add(new Recipe("Axe", new ObservableCollection<KeyValuePair<Item, int>> {
                new KeyValuePair<Item, int>(items.ElementAt(4), 1),
                new KeyValuePair<Item, int>(items.ElementAt(3), 1)
                },
                items.ElementAt(0)) 
            );

            lsvRecips.ItemsSource = recipes;

            rpCbItems.ItemsSource = items;

            cbItems.ItemsSource = items;
            cbQt.ItemsSource = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            inCbItems.ItemsSource = items;
            inCbQt.ItemsSource = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //GENERATE INVETORY

            inventoryStack = new Inventory(
                new List<ItemInventory> {
                    {new ItemInventory(items.ElementAt(0), 2)},
                    {new ItemInventory(items.ElementAt(1), 3)},
                    {new ItemInventory(items.ElementAt(3), 10)},
                    {new ItemInventory(items.ElementAt(4), 7)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                    {new ItemInventory(new Item(0, null, null, null), 0)},
                }
                );

            Inventory.ItemsSource = inventoryStack.InventoryList;

        }

        private void lsvRecips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe r = (Recipe) lsvRecips.SelectedItem;
            Debug.WriteLine("Selected: "+r.Name);

            lsvItemsRecips.ItemsSource = r.Items;

        }

        private void Button_Click_Add_Item(object sender, RoutedEventArgs e)
        {
            if (!(itemName.Text.Trim().Length == 0 && itemDesc.Text.Trim().Length == 0 && itemUrl.Text.Trim().Length == 0))
            {
                Item i = new Item(items.Count(), itemName.Text, itemDesc.Text, itemUrl.Text);

                items.Add(i);

                itemName.Text = "";
                itemDesc.Text = "";
                itemUrl.Text = "";
            }

        }

        private void lsvItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Item i = (Item) lsvItems.SelectedItem;

            if (i != null)
            {
                itemName.Text = i.Name;
                itemDesc.Text = i.Desc;
                itemUrl.Text = i.Url;
            }

        }

        private void Button_Click_Save_Item(object sender, RoutedEventArgs e)
        {
            if(!(itemName.Text.Trim().Length == 0 && itemDesc.Text.Trim().Length == 0 && itemUrl.Text.Trim().Length == 0) ) 
            {
                Item i = (Item)lsvItems.SelectedItem;
                if (i != null)
                {
                    i.Name = itemName.Text;
                    i.Desc = itemDesc.Text;
                    i.Url = itemUrl.Text;
                }

                lsvItems.SelectedItem = null;

                itemName.Text = "";
                itemDesc.Text = "";
                itemUrl.Text = "";
            }

        }

        private void Button_Click_Add_Recipe(object sender, RoutedEventArgs e)
        {
            Item i = (Item)rpCbItems.SelectedItem;
            if (!recipeName.Text.Trim().Equals("") && i != null)
            {
                Recipe r = new Recipe(recipeName.Text, i);
                recipes.Add(r);
                recipeName.Text = "";
                rpCbItems.SelectedIndex = -1;
            }

        }

        private void Button_Click_Add_Item_Recipe(object sender, RoutedEventArgs e)
        {
            
            Item i = (Item)cbItems.SelectedItem;
            int qt = (int)(cbQt.SelectedIndex != -1 ? cbQt.SelectedItem : -1);

            Recipe r = (Recipe)lsvRecips.SelectedItem;


            if (qt != -1 && qt != 0 && i != null && r != null) 
            {
                r.Items.Add(new KeyValuePair<Item, int>(i, qt));   
            }
        }

        private void Button_Click_Add_Item_Inventory(object sender, RoutedEventArgs e)
        {
            Item i = (Item)inCbItems.SelectedItem;
            int qt = (int)(inCbQt.SelectedIndex != -1 ? cbQt.SelectedItem : -1);

            if (qt != 0 && i != null)
            {
                if (inventoryStack.InventoryList.Contains(new ItemInventory(i, qt)))
                {
                    Debug.WriteLine("Añadir la cantidad: ", qt);
                }
                else
                {
                    int x = 0;
                    Debug.WriteLine("COUNT: " + inventoryStack.InventoryList.Count);
                    bool sem = false;

                    do {
                        

                        if (inventoryStack.InventoryList.ElementAt<ItemInventory>(x).Quantity == 0)
                        {
                            Debug.WriteLine("Elemnt at: " + (inventoryStack.InventoryList.ElementAt<ItemInventory>(x).Quantity));
                            Debug.WriteLine("ENter!!");

                            List<ItemInventory> auxList = inventoryStack.InventoryList;

                            auxList.RemoveAt(x);
                            auxList.Insert(x, new ItemInventory(i, qt));

                            inventoryStack.InventoryList = auxList;
                            Inventory.ItemsSource = inventoryStack.InventoryList;
                            sem = true;
                        }

                        x++;

                    } while (x < inventoryStack.InventoryList.Count && !sem);

                }
            }


        }

        private void Button_Click_Delete_Item_Inventory(object sender, RoutedEventArgs e)
        {
            int indexSelected = Inventory.SelectedIndex;
            Debug.WriteLine("Delete index: " + indexSelected);
            if(indexSelected != -1)
            {
                inventoryStack.InventoryList.RemoveAt(indexSelected);
                Inventory.ItemsSource = inventoryStack.InventoryList;
            }

        }

        private void Button_Click_Combine_Items(object sender, RoutedEventArgs e)
        {
            List<ItemInventory> itemsSelected = Inventory.SelectedItems.OfType<ItemInventory>().ToList();

            Debug.WriteLine("COUNT SELECTED ITEMS: " + itemsSelected.Count);

            foreach (Recipe recipe in recipes)
            {
                Debug.WriteLine("RECIPE:: " + recipe.Name+" - "+recipe.Items.Count);
                foreach (KeyValuePair<Item, int> item_recipe in recipe.Items)
                {
                    foreach (ItemInventory item in itemsSelected) 
                    {
                        if(item_recipe.Key == item.Item)
                        {
                            /*
                            Preparar semaforo para que cuando no este a false sea que esta se puede crear
                            emezara el semaforo a true si da una vuelta si cumplir las condiciones no sera una combinacion valida para la receta
                            */
                            Debug.WriteLine("   TRUE " + item.Item + " - " + item.Quantity + " ::: " + item_recipe.Key + " - " + item_recipe.Value);
                        }
                    }
                    
                }
            }

        }
    }
}
