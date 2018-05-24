using RandomAutoClicker.Model.Clicker.Interval;
using System;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickerIntervalFactory : IClickerIntervalFactory
    {
        private readonly IDelayRangeProvider _delayRangeProvider;
        private readonly IFixedDelayProvider _fixedDelayProvider;

        public ClickerIntervalFactory(
            IDelayRangeProvider delayRangeProvider,
            IFixedDelayProvider fixedDelayProvider
            )
        {
            _delayRangeProvider = delayRangeProvider;
            _fixedDelayProvider = fixedDelayProvider;
        }

        public IClickerInterval CreateClickerInterval(ClickDelayEnum clickDelay)
        {
            switch (clickDelay)
            {
                case ClickDelayEnum.Random:
                    var rangeDelay = _delayRangeProvider.GetDelayRange();
                    return new RandomClickerInterval(rangeDelay.From, rangeDelay.To);
                case ClickDelayEnum.Fixed:
                    var fixedDelay = _fixedDelayProvider.GetFixedDelay();
                    return new FixedClickerInterval(fixedDelay.Delay);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public interface IFixedDelayProvider
    {
        FixedDelay GetFixedDelay();
    }

    public class FixedDelayProvider : IFixedDelayProvider
    {
        private readonly FixedDelay _intContainer;

        public FixedDelayProvider()
        {
            _intContainer = new FixedDelay(fixedDelay: 1000, fixedDelayMin: 10) { Delay = 1000 };
        }

        public FixedDelay GetFixedDelay()
        {
            return _intContainer;
        }
    }
}
