using RandomAutoClicker.Model.Clicker.Config;
using RandomAutoClicker.ViewModel;
using System;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickerConfigFactory : IClickerConfigFactory
    {
        private readonly MainWindowViewModel _viewModel;

        public ClickerConfigFactory(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public BaseClickerConfig CreateClickerConfig(ClickAreaEnum clickArea)
        {
            switch (clickArea)
            {
                case ClickAreaEnum.FullScreen:
                    return new FullScreenClickerConfig();
                case ClickAreaEnum.Area:
                    return new AreaClickerConfig(_viewModel.Area.X, _viewModel.Area.Width, _viewModel.Area.Y, _viewModel.Area.Height);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
