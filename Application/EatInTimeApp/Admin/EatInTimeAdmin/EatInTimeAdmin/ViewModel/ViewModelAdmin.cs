using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EatInTimeAdmin.ViewModel
{
    public class ViewModelAdmin : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private EatInTimeContext db = new EatInTimeContext();

        #region Properties
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

        private ObservableCollection<Tables> _Tables;
        public ObservableCollection<Tables> Tables
        {
            get { return _Tables; }
            set
            {
                _Tables = value;
                OnPropertyChanged("Tables");
            }
        }

        private ObservableCollection<Commande> _Commande;
        public ObservableCollection<Commande> Commande
        {
            get { return _Commande; }
            set
            {
                _Commande = value;
                OnPropertyChanged("Commande");
            }
        }
        #endregion

        public ViewModelAdmin()
        {
            using (db)
            {
                Dishes = new ObservableCollection<Plat>(db.Plat);
                Commande = new ObservableCollection<Commande>(db.Commande);
            }
        }
    }
}
