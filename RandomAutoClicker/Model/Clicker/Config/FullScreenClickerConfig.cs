namespace RandomAutoClicker.Model.Clicker.Config
{
    public class FullScreenClickerConfig : BaseClickerConfig
    {
        private readonly int _height;
        private readonly int _width;

        public FullScreenClickerConfig()
        {
            var screen  = WpfScreen.Primary;
            _height = (int)screen.DeviceBounds.Height;
            _width = (int)screen.DeviceBounds.Width;
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
            return 0;
        }

        public override int GetYStart()
        {
            return 0;
        }
    }
}
