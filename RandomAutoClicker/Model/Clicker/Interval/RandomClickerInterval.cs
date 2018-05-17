using System;

namespace RandomAutoClicker.Model.Clicker.Interval
{
    public class RandomClickerInterval : IClickerInterval
    {
        private readonly Random _random;
        private readonly int _start;
        private readonly int _end;

        public RandomClickerInterval(int start, int end)
        {
            _start = start;
            _end = end;
            _random = new Random();
        }

        public int GetNextInterval()
        {
            return _random.Next(_start, _end);
        }
    }
}
