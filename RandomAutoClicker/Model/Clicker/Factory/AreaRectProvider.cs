namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class AreaRectProvider : IAreaRectProvider
    {
        private readonly AreaRect _areaRect;

        public AreaRectProvider()
        {
            //TODO: move to constants
            _areaRect = new AreaRect(10, 10)
            {
                Height = 100,
                Width = 100
            };
        }

        public AreaRect GetAreaRect()
        {
            return _areaRect;
        }
    }
}
