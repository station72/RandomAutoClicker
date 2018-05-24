using RandomAutoClicker.ViewModel;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class DelayRangeProvider : IDelayRangeProvider
    {
        private readonly Range _range;

        public DelayRangeProvider()
        {
            _range = new Range
            {
                //TODO: move to constants
                To = 100,
                From = 50
            };
        }

        public Range GetDelayRange()
        {
            return _range;
        }
    }
}
