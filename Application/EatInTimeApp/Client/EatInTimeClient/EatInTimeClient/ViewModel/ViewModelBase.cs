using System.ComponentModel;

namespace EatInTimeClient.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
