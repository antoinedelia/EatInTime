using System.Collections.ObjectModel;
using EatInTimeClient.DAL;
using System.Linq;
using System.Windows.Input;
using EatInTimeClient.Helpers;
using System;
using Microsoft.Win32;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelOrder : ViewModelBase, INotifyPropertyChanged
    {
        ViewModelBase vmBase = new ViewModelBase();

        private EatInTimeContext db = new EatInTimeContext();
        #region Properties
        public ObservableCollection<Plat> AllDishes { get; set; }
        private ObservableCollection<Tables> AllTables;

        //internal void OnWindowClosing(object sender, CancelEventArgs e)
        //{
        //    DoCloseWindow();
        //}

        Tables CurrentTable;
        private ObservableCollection<Avancement> Avancements;
        #endregion
        #region Accessors

        private int TableNumber;

        private Commande _Command;
        public Commande Command
        {
            get; set;
        }

        

        private ObservableCollection<Plat> _Dishes;
        public ObservableCollection<Plat> Dishes
        {
            get { return _Dishes; }
            set
            {
                _Dishes = value;
                OnPropertyChanged("Dishes");
            }
        }

        private ObservableCollection<Plat> _Entrees;
        public ObservableCollection<Plat> Entrees
        {
            get { return _Entrees; }
            set
            {
                _Entrees = value;
                OnPropertyChanged("Entrees");
            }
        }

        private ObservableCollection<Plat> _Desserts;
        public ObservableCollection<Plat> Desserts
        {
            get { return _Desserts; }
            set
            {
                _Desserts = value;
                OnPropertyChanged("Desserts");
            }
        }

        private ObservableCollection<Plat> _Drinks;
        public ObservableCollection<Plat> Drinks
        {
            get { return _Drinks; }
            set
            {
                _Drinks = value;
                OnPropertyChanged("Drinks");
            }
        }

        private static ObservableCollection<Plat> _Order;
        public ObservableCollection<Plat> Order
        {
            get { return _Order; }
            set
            {
                _Order = value;
                OnPropertyChanged("Order");
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
            }
        }

        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get { return _TotalPrice; }
            set
            {
                _TotalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        #endregion

        #region Commands
        //private RelayCommand _CloseWindowCommand;
        //public RelayCommand CloseWindowCommand
        //{
        //    get
        //    {
        //        if(_CloseWindowCommand == null)
        //        {
        //            _CloseWindowCommand = new RelayCommand(DoCloseWindow);
        //        }
        //        return _CloseWindowCommand;
        //    }
        //}

        private RelayCommand _validateCommand;
        public RelayCommand ValidateCommand
        {
            get
            {
                if(_validateCommand == null)
                {
                    _validateCommand = new RelayCommand<object>(DoValidateCommand);
                }
                return _validateCommand;
            }
        }

        private RelayCommand _addEntreeToCommand;
        public RelayCommand AddEntreeToCommand
        {
            get
            {
                if (_addEntreeToCommand == null)
                {
                    _addEntreeToCommand = new RelayCommand<object>(DoAddEntree);
                }
                return _addEntreeToCommand;
            }
        }

        private RelayCommand _addDishToCommand;
        public RelayCommand AddDishToCommand
        {
            get
            {
                if(_addDishToCommand == null)
                {
                    _addDishToCommand = new RelayCommand<object>(DoAddDish);
                }
                return _addDishToCommand;
            }

        }

        private RelayCommand _addDessertToCommand;
        public RelayCommand AddDessertToCommand
        {
            get
            {
                if(_addDessertToCommand == null)
                {
                    _addDessertToCommand = new RelayCommand<object>(DoAddDessert);
                }
                return _addDessertToCommand;
            }
        }

        private RelayCommand _addDrinkToCommand;
        public RelayCommand AddDrinkToCommand
        {
            get
            {
                if(_addDrinkToCommand == null)
                {
                    _addDrinkToCommand = new RelayCommand<object>(DoAddDrink);
                }
                return _addDrinkToCommand;
            }
        }

        private RelayCommand _searchDish;
        public RelayCommand SearchDish
        {
            get
            {
                if(_searchDish == null)
                {
                    _searchDish = new RelayCommand<object>(DoSearchDish);
                }
                return _searchDish;
            }
        }

        #endregion

        public ViewModelOrder()
        {
            using (db)
            {
                AllDishes = new ObservableCollection<Plat>(db.Plat);
                AllDishes = EditIngredients(AllDishes);
                Avancements = new ObservableCollection<Avancement>(db.Avancement);
                Dishes = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Plat"));
                Entrees = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Entrée"));
                Desserts = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Dessert"));
                Drinks = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Boisson"));

                //Table Management, working
                //AllTables = new ObservableCollection<Tables>(db.Tables);
                //if(CurrentTable == null && AllTables.Any(n => n.Est_Occupee == false))
                //{
                //    CurrentTable = AllTables.Where(n => n.Est_Occupee == false).First();
                //    CurrentTable.Est_Occupee = true;
                //    TableNumber = db.Tables.First().Numero_Table;
                //    db.Entry(CurrentTable).State = System.Data.Entity.EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
        }

        #region DoMethods
        private void DoCloseWindow()
        {
            using (var db = new EatInTimeContext())
            {
                CurrentTable.Est_Occupee = false;
                db.Entry(CurrentTable).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void DoSearchDish(object obj)
        {
            string search = obj.ToString();

            Dishes = new ObservableCollection<Plat>(AllDishes.Where(n => n.Nom_Plat.Contains(search)));
        }

        private void DoAddEntree(object obj)
        {
            int index = (int)obj;
            Plat plat = Entrees[index];
            if (Order == null)
            {
                Order = new ObservableCollection<Plat>();
            }
            Order.Add(plat);
            TotalPrice += plat.Prix_Plat;
        }

        private void DoAddDish(object obj)
        {
            int index = (int)obj;
            Plat plat = Dishes[index];
            if (Order == null)
            {
                Order = new ObservableCollection<Plat>();
            }
            Order.Add(plat);
            TotalPrice += plat.Prix_Plat;
        }

        private void DoAddDessert(object obj)
        {
            int index = (int)obj;
            Plat plat = Desserts[index];
            if (Order == null)
            {
                Order = new ObservableCollection<Plat>();
            }
            Order.Add(plat);
            TotalPrice += plat.Prix_Plat;
        }

        private void DoAddDrink(object obj)
        {
            int index = (int)obj;
            Plat plat = Drinks[index];
            if (Order == null)
            {
                Order = new ObservableCollection<Plat>();
            }
            Order.Add(plat);
            TotalPrice += plat.Prix_Plat;
        }

        private void DoValidateCommand(object obj)
        {
            Command = new Commande();
            
            //Command.Id_Table = CurrentTable.Numero_Table; //Table management, working
            
            try
            {
                using(var dbAdd = new EatInTimeContext())
                {
                    

                    Command.Avancement = Avancements.FirstOrDefault(n => n.Nom_Avancement == "Commande");
                    Command.Id_Avancement = Command.Avancement.Id_Avancement;
                    Command.Plat = Order;
                    Command.Date_Commande = DateTime.Now;

                    Command.Id_Table = 1;
                    dbAdd.Commande.Attach(Command);
                    dbAdd.Commande.Add(Command);
                    dbAdd.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion
        private ObservableCollection<Plat> EditIngredients(ObservableCollection<Plat> ListePlats)
        {
            foreach (Plat Plat in ListePlats)
	        {
                ObservableCollection<Ingredient> listeIngredients = (ObservableCollection<Ingredient>)Plat.Ingredients;
                foreach (Ingredient item in listeIngredients)
                {
                    Plat.String_Ingredients += item.Nom_Ingredient + ", ";
                }
                if (Plat.String_Ingredients != null) Plat.String_Ingredients = Plat.String_Ingredients.Remove((Plat.String_Ingredients.Length - 2));
                //Plat.String_Ingredients = Plat.Ingredients.OfType<string>();//Plat.Ingredients.ToString();
	        }
            return ListePlats;
        }

        //public void Dispose()
        //{
            
        //}


        #region RelayCommandManager

        private bool canExecute = true;

        private class RelayCommand<T> : RelayCommand
        {
            private RelayCommand addDishToCommand;

            public RelayCommand(Action<object> execute) : base(execute)
            {
            }
        }

        public bool CanExecute
        {
            get
            {
                return canExecute;
            }
            set
            {
                if (canExecute == value)
                {
                    return;
                }
                canExecute = value;
            }
        }


        #endregion
    }
}