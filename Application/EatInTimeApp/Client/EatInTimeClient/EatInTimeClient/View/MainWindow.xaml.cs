using System.Windows;

namespace EatInTimeClient.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var vm = new EatInTimeClient.ViewModel.ViewModelMainWindow();
            //using (var db = new ModelContext())
            //{
            //    var tg = db.Plat.FirstOrDefault();
            //    Console.Write("");
            //}
        }

        private void OrderDishesControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ViewModelOrder orderViewModel = new ViewModel.ViewModelOrder();
            orderViewModel.LoadDishes();
        }
    }
}
