using RandomAutoClicker.Infrastructure;

namespace RandomAutoClicker.Model
{
    public class FixedDelay : ObservableObject
    {
        private readonly int _fixedDelayMin;
        private int _fixedDelay;

        public FixedDelay(
            int fixedDelay,
            int fixedDelayMin)
        {
            _fixedDelay = fixedDelay;
            _fixedDelayMin = fixedDelayMin;
        }

        public int Delay
        {

            get { return _fixedDelay; }
            set
            {
                if (value < _fixedDelayMin)
                    value = _fixedDelayMin;

                _fixedDelay = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(Delay));
            }
        }

        public int FixedDelayMin { get { return _fixedDelayMin; } }
    }
}