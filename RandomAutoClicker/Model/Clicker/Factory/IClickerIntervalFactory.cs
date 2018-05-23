using RandomAutoClicker.Model.Clicker.Interval;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public interface IClickerIntervalFactory
    {
        IClickerInterval CreateClickerInterval(ClickDelayEnum clickDelay);
    }
}
