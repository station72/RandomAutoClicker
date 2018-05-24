using RandomAutoClicker.Model.Clicker.Config;
using System;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickerConfigFactory : IClickerConfigFactory
    {
        private readonly IAreaRectProvider _areaRectProvider;

        public ClickerConfigFactory(IAreaRectProvider areaRectProvider)
        {
            _areaRectProvider = areaRectProvider;
        }

        public BaseClickerConfig CreateClickerConfig(ClickAreaEnum clickArea)
        {
            switch (clickArea)
            {
                case ClickAreaEnum.FullScreen:
                    return new FullScreenClickerConfig();
                case ClickAreaEnum.Area:
                    var rectArea = _areaRectProvider.GetAreaRect();
                    return new AreaClickerConfig(rectArea.X, rectArea.Width, rectArea.Y, rectArea.Height);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
