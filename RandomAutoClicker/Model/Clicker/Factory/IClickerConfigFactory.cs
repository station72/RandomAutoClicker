using RandomAutoClicker.Model.Clicker.Config;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public interface IClickerConfigFactory
    {
        BaseClickerConfig CreateClickerConfig(ClickAreaEnum clickArea);
    }
}
