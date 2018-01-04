using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace EatInTimeClient.View
{
    /// <summary>
    /// Logique d'interaction pour UserControlOrdering.xaml
    /// </summary>
    public partial class UserControlOrdering : UserControl
    {
        public UserControlOrdering()
        {
            InitializeComponent();
            //ViewModel.ViewModelOrder vmorder = new ViewModel.ViewModelOrder();
            //DishesItemsControl.ItemsSource = vmorder.Dishes;
            ////EntreesItemsControl.ItemsSource = vmorder.Entrees;
            //DessertsItemsControl.ItemsSource = vmorder.Desserts;
            //DrinkssItemsControl.ItemsSource = vmorder.Drinks;
        }

        private void SearchDishTextBox_GotFocus(object sender, RoutedEventArgs e) => SearchDishTextBox.Text = String.Empty;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchDishTextBox_LostFocus(object sender, RoutedEventArgs e) => SearchDishTextBox.Text = "Rechercher un plat";
    }
}