using RandomAutoClicker.Infrastructure;

namespace RandomAutoClicker.Model
{
    public class AreaRect : ObservableObject
    {
        private readonly int _minWidthSize;
        private readonly int _minHeightSize;

        public AreaRect(int minWidthSize, int minHeightSize)
        {
            _minWidthSize = minWidthSize;
            _minHeightSize = minHeightSize;
        }

        private int _x;
        public int X
        {
            get { return _x; }
            set
            {
                if (value < 0)
                    value = 0;

                _x = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(X));
            }
        }

        private int _y;
        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                    value = 0;

                _y = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(Y));
            }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                if (value < _minWidthSize)
                    value = _minWidthSize;

                _width = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(Width));
            }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                if (value < 0)
                    value = 0;

                _height = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(Height));
            }
        }

        public bool WidthCanBeReduced()
        {
            return Width > _minWidthSize;
        }

        public bool HeightCanBeReduced()
        {
            return Height > _minHeightSize;
        }
    }
}
