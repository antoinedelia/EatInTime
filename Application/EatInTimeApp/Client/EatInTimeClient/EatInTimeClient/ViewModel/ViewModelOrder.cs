using System.Collections.ObjectModel;
using EatInTimeClient.DAL;
using System.Linq;
using System.Windows.Input;
using EatInTimeClient.Helpers;
using System;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelOrder : ViewModelBase
    {
        public ObservableCollection<Plat> AllDishes { get; set; }
        public ObservableCollection<Plat> Dishes { get; set; }
        public ObservableCollection<Plat> Entrees { get; set; }
        public ObservableCollection<Plat> Desserts { get; set; }
        public ObservableCollection<Plat> Drinks { get; set; }
        object _SelectedDish;

        public Command AddToOrderCommand { get; set; }

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

            AddToOrderCommand = new Command
            {
                CanExecuteFunc = obj => true,
                ExecuteFunc = AddToOrder
            };
        }

        private void AddToOrder(object obj)
        {
            throw new NotImplementedException();
        }

        //public ICommand AddToOrder{ get; set; }

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
    }
}
