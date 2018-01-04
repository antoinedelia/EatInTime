using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EatInTimeClient.Helpers
{
    public class TestCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;
        private event EventHandler CanExecuteChangedInternal;

        public TestCommand(Action<object> execute) : this(execute, DefaultCanExecute)
        {

        }

        public TestCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        //private Action _action;
        //private bool _canExecute;

        //public RelayCommand(Action action, bool canExecute)
        //{
        //    _action = action;
        //    _canExecute = canExecute;
        //}
        //public bool CanExecute(object parameter)
        //{
        //    return _canExecute;
        //}

        //public event EventHandler CanExecuteChanged;

        //public void Execute(object parameter)
        //{
        //    _action();
        //}


        //OLD CODE BELOW

        //public event EventHandler CanExecuteChanged;

        //public Predicate<object> CanExecuteFunc { get; set; }
        //public Action<object> ExecuteFunc { get; set; }

        //public bool CanExecute(object parameter)
        //{
        //    return CanExecuteFunc(parameter);
        //}

        //public void Execute(object parameter)
        //{
        //    ExecuteFunc(parameter);
        //}
    }
}
