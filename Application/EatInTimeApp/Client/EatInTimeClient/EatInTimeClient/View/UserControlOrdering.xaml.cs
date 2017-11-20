using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EatInTimeClient.Model;

namespace EatInTimeClient.View
{
    /// <summary>
    /// Logique d'interaction pour UserControlOrdering.xaml
    /// </summary>
    public partial class UserControlOrdering : UserControl
    {
        public ObservableCollection<Plat> Liste_Plat { get; private set; }

        public UserControlOrdering()
        {
            InitializeComponent();
            ViewModel.ViewModelOrder vmorder = new ViewModel.ViewModelOrder();
            DishesItemsControl.ItemsSource = vmorder.Dishes;
            //DishesItemsControl.ItemsSource = Liste_Plat = new ObservableCollection<Plat>();
            
            //Liste_Plat = vmorder.Dishes;
            //Model.Plat listePlats = new Plat();
            //Liste_Plat.Add(new Plat("Pâtes Carbonara", 8.50m));
            //Liste_Plat.Add(new Plat("Lasagnes", 12.70m));
        }

        private void SearchDishTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchDishTextBox.Text = String.Empty;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}