using System.ComponentModel;
using System.Windows.Input;

namespace RandomAutoClicker.Infrastructure
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InvalidateRequerySuggested()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
