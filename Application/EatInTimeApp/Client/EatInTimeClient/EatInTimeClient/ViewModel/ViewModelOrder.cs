using System.Collections.ObjectModel;
using EatInTimeClient.DAL;
using System.Linq;
using System.Windows.Input;
using EatInTimeClient.Helpers;
using System;
using Microsoft.Win32;
using System.Windows;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelOrder : ViewModelBase
    {
        public ObservableCollection<Plat> AllDishes { get; set; }
        public ObservableCollection<Plat> Dishes { get; set; }
        private ObservableCollection<Plat> _Entrees;
        public ObservableCollection<Plat> Entrees
        {
            get { return _Entrees; }
            set { _Entrees = value; }
        }
        public ObservableCollection<Plat> Desserts { get; set; }
        public ObservableCollection<Plat> Drinks { get; set; }
        object _SelectedDish;
        private ObservableCollection<Plat> _Order;
        public ObservableCollection<Plat> Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
            }
        }
        private RelayCommand _addEntreeToCommand;
        public RelayCommand AddEntreeToCommand
        {
            get
            {
                if (_addEntreeToCommand == null)
                {
                    _addEntreeToCommand = new RelayCommand<object>(DoAdd);
                }
                return _addEntreeToCommand;
            }
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
            Order = new ObservableCollection<Plat>();
        }

        private void DoAdd(object obj)
        {
            int index = (int)obj;
            Plat plat = Entrees[index];
            _Order.Add(plat);
        }
        
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
                Test = "Test";
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