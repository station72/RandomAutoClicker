namespace RandomAutoClicker.Model.Clicker.Config
{
    public class AreaClickerConfig : BaseClickerConfig
    {
        private readonly int _xStart;
        private readonly int _width;
        private readonly int _yStart;
        private readonly int _height;

        public AreaClickerConfig(int xStart, int width, int yStart, int height)
        {
            _xStart = xStart;
            _width = width;
            _yStart = yStart;
            _height = height;
        }

        public override int GetHeight()
        {
            return _height;
        }

        public override int GetWidth()
        {
            return _width;
        }

        public override int GetXStart()
        {
            return _xStart;
        }

        public override int GetYStart()
        {
            return _yStart;
        }
    }
}
