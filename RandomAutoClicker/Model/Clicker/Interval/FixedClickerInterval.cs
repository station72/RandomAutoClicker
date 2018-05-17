namespace RandomAutoClicker.Model.Clicker.Interval
{
    public class FixedClickerInterval : IClickerInterval
    {
        private readonly int _interval;

        public FixedClickerInterval(int interval)
        {
            _interval = interval;
        }

        public int GetNextInterval()
        {
            return _interval;
        }
    }
}
