using RandomAutoClicker.ViewModel;
using System;
using System.Windows;

namespace RandomAutoClicker
{
    public partial class MainWindow : Window
    {
        public MainWindow(
            MainWindowViewModel viewModel
            )
        {
            InitializeComponent();
            DataContext = viewModel ?? throw new NullReferenceException(nameof(viewModel));
        }
    }
}
