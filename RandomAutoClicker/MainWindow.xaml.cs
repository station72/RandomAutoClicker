using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace RandomAutoClicker
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewmodel;

        public MainWindow()
        {
            InitializeComponent();
            _viewmodel = new MainWindowViewModel(Dispatcher, new EventEntityFactory<ClickerEventArgs>());
            DataContext = _viewmodel;
        }
    }
}
