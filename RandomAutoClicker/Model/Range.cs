using RandomAutoClicker.Infrastructure;

namespace RandomAutoClicker.Model
{
    public class Range : ObservableObject
    {
        private int _from;
        public int From
        {
            get { return _from; }
            set
            {
                if (value < 0)
                    value = 0;

                if (value > To)
                    value = To;

                _from = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(From));
            }
        }

        private int _toMin = 50;
        private int _to;
        public int To
        {
            get { return _to; }
            set
            {
                if (value < _toMin)
                    value = _toMin;

                _to = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(To));
            }
        }

        public bool FromCanBeIncreased()
        {
            return From < To;
        }

        public bool ToCanBeReduced()
        {
            return To > _toMin && To > From;
        }
    }
}
