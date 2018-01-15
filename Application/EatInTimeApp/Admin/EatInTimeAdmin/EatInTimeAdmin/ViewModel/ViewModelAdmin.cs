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
using System.Windows;

namespace EatInTimeAdmin.ViewModel
{
    public class ViewModelAdmin : ViewModelBase, INotifyPropertyChanged
    {
        private EatInTimeContext db = new EatInTimeContext();
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
            while(true)
            {
                using (var asyncdb = new EatInTimeContext())
                {
                    AllTables = new AsyncObservableCollection<EatInTimeAdmin.Tables>(asyncdb.Tables);

                    //AllTables[0].XPos = 20;
                    //AllTables[0].YPos = 20;
                    //AllTables[1].XPos = 150;
                    //AllTables[1].YPos = 150;
                    foreach (Tables item in AllTables)
                    {
                        if (item.Est_Occupee == true)
                            item.Couleur = Brushes.Green;
                        else
                            item.Couleur = Brushes.Gray;
                    }
                    int number = 0;
                    if (isNotified = AllTables.Any(n => n.Est_Occupee == true))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            foreach (Tables item in AllTables)
                            {
                                item.Couleur = Brushes.Green;
                                if (item.Alerte == true)
                                {
                                    item.Couleur = Brushes.Red;
                                    number = item.Numero_Table;
                                    List<Commande> tempCom = item.Commande.ToList();
                                    for (int i = tempCom.Count - 1; i >= 0; i--)
                                    {
                                        if (tempCom[i].Id_Avancement != 2)
                                        {
                                            tempCom.RemoveAt(i);
                                        }
                                        else
                                        {
                                            foreach (Plat plat in tempCom[i].Plat)
                                            {
                                                tempCom[i].String_Plats += plat.Nom_Plat + " ,";
                                            }
                                            if (tempCom[i].String_Plats != null)
                                            {
                                                tempCom[i].String_Plats = tempCom[i].String_Plats.Remove((tempCom[i].String_Plats.Length - 2));
                                                tempCom[i].String_Plats = "Commande en cours : " + tempCom[i].String_Plats;
                                            }
                                        }
                                    }
                                    item.Commande = tempCom;

                                    //foreach (Commande com in item.Commande)
                                    //{
                                    //    if (com.Id_Avancement != 2)
                                    //    {
                                    //        item.Commande.Remove(com);
                                    //    }
                                    //}
                                    // MessageBox.Show(string.Format("La table n° {0} Appelle un serveur", number)); 
                                }
                                else
                                {
                                    item.Couleur = Brushes.Green;
                                }
                                //TablesToCheck.Clear();
                                //TablesToCheck.Add(item);
                            }

                            //foreach (Tables item in AllTables.Where(n => n.Alerte == true))
                            //{
                            //    item.Couleur = Brushes.Red;
                            //    TablesToCheck.Add(item);
                            //}
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
                    AllTables2 = AllTables;
                }
            }
        }

        private RelayCommand _extinctAlert;
        public RelayCommand ExtinctAlert
        {
            get
            {
                if (_extinctAlert == null) _extinctAlert = new RelayCommand<object>(DoExtinctAlert);
                return _extinctAlert;
            }
        }

        private void DoExtinctAlert(object obj)
        {
            int tableNumber = (int)obj;
            
            using (var db2 = new EatInTimeContext())
            {
                AllTables = new AsyncObservableCollection<Tables>(db2.Tables);
                Tables tempTable = AllTables[tableNumber];
                tempTable.Alerte = false;
                //AllTables[tableNumber].Couleur = Brushes.Green;
                db2.Entry(tempTable).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                //AllTables = new AsyncObservableCollection<Tables>(db2.Tables);
                //GC.KeepAlive(AllTables);
            }
           
            //DbChecking();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private AsyncObservableCollection<Tables> _AllTables;
        public AsyncObservableCollection<Tables> AllTables
        {
            get { return _AllTables; }
            set
            {
                _AllTables = value;
                OnPropertyChanged("AllTables");
            }
        }

        private ObservableCollection<Tables> _AllTables2;
        public ObservableCollection<Tables> AllTables2
        {
            get { return _AllTables2; }
            set
            {
                _AllTables2 = value;
                OnPropertyChanged("AllTables2");
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

        public ObservableCollection<Tables> stockTables { get; set; }

        private Thread NotificationCheckThread;
        public ViewModelAdmin()
        {
            stockTables = new ObservableCollection<Tables>();
            TablesToCheck = new AsyncObservableCollection<Tables>();
            RowColor = Brushes.Blue;
            AllTables2 = new ObservableCollection<EatInTimeAdmin.Tables>();
            using (db)
            {
                Dishes = new ObservableCollection<Plat>(db.Plat);
                Commande = new ObservableCollection<Commande>(db.Commande);
            }
            //StartOnDifferentThread();
            NotificationCheckThread = new Thread(DbChecking)
            {
                IsBackground = true
            };
            NotificationCheckThread.Start();
        }

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
    }
}
