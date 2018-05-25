using RandomAutoClicker.ViewModel;
using System;
using System.Windows;

namespace RandomAutoClicker
{
    public partial class MainWindow : Window, IDisposable
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(
            MainWindowViewModel viewModel
            )
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new NullReferenceException(nameof(viewModel));
            DataContext = _viewModel;
        }

        public void Dispose()
        {
            _viewModel.Dispose();
        }
    }
}
