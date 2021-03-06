﻿using System.Collections.ObjectModel;
using EatInTimeClient.Model;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelOrder : ViewModelBase
    {
        public ObservableCollection<Plat> Dishes { get; set; }

        object _SelectedDish;

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
            using (var db = new ModelContext())
            {
                Dishes = new ObservableCollection<Plat>(db.Plat);
            }
        }
    }
}
