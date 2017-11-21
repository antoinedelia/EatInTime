using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string property)
        {
            if(PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(property)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
