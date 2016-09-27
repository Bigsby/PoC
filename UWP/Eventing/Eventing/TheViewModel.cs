using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Eventing
{
    public class TheViewModel : INotifyPropertyChanged, Caliburn.Micro.IHandle<CaliburnEvent>
    {
        private readonly Caliburn.Micro.IEventAggregator _caliburn;
        private readonly Oneview.Client.Base.Events.IEventAggregator _prism;
        private readonly Stopwatch _stopwatch;
        public TheViewModel()
        {
            _stopwatch = new Stopwatch();
            _caliburn = new Caliburn.Micro.EventAggregator();
            _caliburn.Subscribe(this);

            _prism = new Oneview.Client.Base.Events.EventAggregator();
            _prism.GetEvent<PrismEvent>().Subscribe(UpdateValue);
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                RaisePropertyChanged();
            }
        }

        public ICommand PublishCaliburn
        {
            get
            {
                return new ActionCommand(p =>
                {
                    _stopwatch.Restart();
                    _caliburn.Publish(new CaliburnEvent("Caliburn was published!"));
                });
            }
        }

        public ICommand PublishPrism
        {
            get
            {
                return new ActionCommand(p =>
                {
                    _stopwatch.Restart();
                    _prism.GetEvent<PrismEvent>().Publish("Prims was published!");
                    
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Handle(CaliburnEvent message)
        {
            UpdateValue(message.Message);
        }

        private void UpdateValue(string value)
        {
            _stopwatch.Stop();
            Value += $"{Environment.NewLine}{value} ({_stopwatch.ElapsedTicks})";
        }
    }

    public class PrismEvent : Oneview.Client.Base.Events.PubSubEvent<string>
    { }

    public class CaliburnEvent
    {
        public CaliburnEvent(string message)
        {
            Message = message;
        }
        public string Message { get; private set; }
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

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute(parameter);
        }
    }
}
