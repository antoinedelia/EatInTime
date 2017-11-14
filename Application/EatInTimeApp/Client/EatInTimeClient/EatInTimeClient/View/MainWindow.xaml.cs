using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EatInTimeClient.ViewModel;
using EatInTimeClient.Model;

namespace EatInTimeClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            var vm = new EatInTimeClient.ViewModel.ViewModelMainWindow();
            //using (var db = new ModelContext())
            //{
            //    var tg = db.Plat.FirstOrDefault();
            //    Console.Write("");
            //}
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
