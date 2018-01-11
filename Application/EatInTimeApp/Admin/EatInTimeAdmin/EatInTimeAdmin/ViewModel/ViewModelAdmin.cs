using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using EatInTimeAdmin.Helpers;
using System.Windows.Media;

namespace EatInTimeAdmin.ViewModel
{
    public class ViewModelAdmin : ViewModelBase, INotifyPropertyChanged
    {
        private bool isNotified;

        void StartOnDifferentThread()
        {
            Task.Factory.StartNew(() =>
                {
                    DbChecking();
                })
                .ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {

                    }
                });
        }

        private void DbChecking()
        {
            while (true)
            {
                using (var asyncdb = new EatInTimeContext())
                {
                    AllTables = new ObservableCollection<EatInTimeAdmin.Tables>(asyncdb.Tables);
                    if (isNotified = AllTables.Any(n => n.Alerte == true))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            TablesToCheck.Clear();
                            foreach (Tables item in AllTables.Where(n => n.Alerte == true))
                                TablesToCheck.Add(item);
                            RowColor = Brushes.Red;
                        });
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            TablesToCheck.Clear();
                        });
                    }

                    Thread.Sleep(3000);
                    //return Tables.Any(n => n.Alerte == true);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private EatInTimeContext db = new EatInTimeContext();

        #region Properties
        private AsyncObservableCollection<Tables> _TablesToCheck;
        public AsyncObservableCollection<Tables> TablesToCheck
        {
            get { return _TablesToCheck; }
            set
            {
                _TablesToCheck = value;
                OnPropertyChanged("TablesToCheck");
            }
        }

        private Brush _RowColor;
        public Brush RowColor
        {
            get { return _RowColor; }
            set
            {
                _RowColor = value;
                OnPropertyChanged("RowColor");
            }
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

        private ObservableCollection<Tables> _AllTables;
        public ObservableCollection<Tables> AllTables
        {
            get { return _AllTables; }
            set
            {
                _AllTables = value;
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
            TablesToCheck = new AsyncObservableCollection<Tables>();
            RowColor = Brushes.Blue;
            using (db)
            {
                Dishes = new ObservableCollection<Plat>(db.Plat);
                Commande = new ObservableCollection<Commande>(db.Commande);
            }
            //StartOnDifferentThread();
            var NotificationCheckThread = new Thread(DbChecking)
            {
                IsBackground = true
            };
            NotificationCheckThread.Start();
        }
    }
}
