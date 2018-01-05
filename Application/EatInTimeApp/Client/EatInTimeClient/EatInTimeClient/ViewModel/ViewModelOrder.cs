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

namespace EatInTimeClient.ViewModel
{
    public class ViewModelOrder : ViewModelBase, INotifyPropertyChanged
    {
        ViewModelBase vmBase = new ViewModelBase();
        public ObservableCollection<Plat> AllDishes { get; set; }
        public ObservableCollection<Plat> Dishes { get; set; }
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
        public ObservableCollection<Plat> Desserts { get; set; }
        public ObservableCollection<Plat> Drinks { get; set; }
        object _SelectedDish;
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

        private void DoSearchDish(object obj)
        {
            string search = obj.ToString();

            Dishes = new ObservableCollection<Plat>(AllDishes.Where(n => n.Nom_Plat.Contains(search)));
        }

        private bool canExecute = true;

        public bool CanExecute
        {
            get
            {
                return canExecute;
            }
            set
            {
                if(canExecute == value)
                {
                    return;
                }
                canExecute = value;
            }
        }
        
        //public ICommand AddToOrderCommand
        //{
        //    get
        //    {
        //        return addToOrderCommand;
        //    }
        //    set
        //    {
        //        addToOrderCommand = value;
        //    }
        //}

        //public ICommand AddToOrderCommand{get{return new RelayCommand(() => AddToOrder(), true); } }
        //public Command AddToOrderCommand { get; set; }

        public object SelectedDish
        {
            get
            {
                return _SelectedDish;
            }
            set
            {
                if(_SelectedDish != value)
                {
                    _SelectedDish = value;
                    RaisePropertyChanged("SelectedDish");
                }
            }
        }

        internal void LoadDishes()
        {
            
        }

        public ViewModelOrder()
        {
            using (var db = new EatInTimeContext())
            {
                AllDishes = new ObservableCollection<Plat>(db.Plat);
                AllDishes = EditIngredients(AllDishes);
                Dishes = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Plat"));
                Entrees = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Entrée"));
                Desserts = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Dessert"));
                Drinks = new ObservableCollection<Plat>(AllDishes.Where(n => n.Type_Plat.Nom_Type_Plat == "Boisson"));
            }
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

        //private void Adding_Plats(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    foreach(Plat item in e.NewItems)
        //    {
        //        item.PropertyChanged += NewItemPropertyChanged;
        //    }
        //}

        //private void NewItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //static void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.OldItems != null)
        //    {
        //        foreach (INotifyPropertyChanged item in e.OldItems)
        //            item.PropertyChanged -= item_PropertyChanged;
        //    }
        //    if (e.NewItems != null)
        //    {
        //        foreach (INotifyPropertyChanged item in e.NewItems)
        //            item.PropertyChanged += item_PropertyChanged;
        //    }
        //}

        //static void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

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

        private class RelayCommand<T> : RelayCommand
        {
            private RelayCommand addDishToCommand;

            //public RelayCommand(RelayCommand addDishToCommand)
            //{
            //    this.addDishToCommand = addDishToCommand;
            //}

            public RelayCommand(Action<object> execute) : base(execute)
            {
            }
        }
    }
}