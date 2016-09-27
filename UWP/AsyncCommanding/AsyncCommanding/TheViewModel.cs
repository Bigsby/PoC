using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace AsyncCommanding
{
    public class TheViewModel : INotifyPropertyChanged
    {
        private int _timer;
        private int _counter;

        public TheViewModel()
        {
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, e) => Timer++;
            dispatcherTimer.Start();
        }

        public int Counter
        {
            get
            {
                return _counter;
            }

            set
            {
                if (_counter == value) return;
                _counter = value;
                RaisePropertyChanged();
            }
        }

        public int Timer
        {
            get
            {
                return _timer;
            }

            set
            {
                if (_timer == value) return;
                _timer = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Add
        {
            get
            {
                return new ActionCommand(p => {
                    Counter++;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            //Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => );
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ActionCommand : ICommand
    {
        private readonly Action<object> _execute;

        public ActionCommand(Action<object> execute)
        {
            _execute = execute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return null != _execute;
        }

        public async void Execute(object parameter)
        {
            if (CanExecute(parameter))
                await Task.Run(() => _execute(parameter));
        }
    }
}
