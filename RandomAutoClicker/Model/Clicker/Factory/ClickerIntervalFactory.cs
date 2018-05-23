using RandomAutoClicker.Model.Clicker.Interval;
using RandomAutoClicker.ViewModel;
using System;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickerIntervalFactory : IClickerIntervalFactory
    {
        private readonly MainWindowViewModel _viewModel;

        public ClickerIntervalFactory(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IClickerInterval CreateClickerInterval(ClickDelayEnum clickDelay)
        {
            switch (clickDelay)
            {
                case ClickDelayEnum.Random:
                    return new RandomClickerInterval(_viewModel.DelayRange.From, _viewModel.DelayRange.To);
                case ClickDelayEnum.Fixed:
                    return new FixedClickerInterval(_viewModel.FixedDelay);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
