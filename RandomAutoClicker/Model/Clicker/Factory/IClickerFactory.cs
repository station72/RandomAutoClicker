namespace RandomAutoClicker.Model.Clicker.Factory
{
    public interface IClickerFactory
    {
        IMouseClicker CreateClicker(ClickDelayEnum clickDelay, ClickAreaEnum clickArea, ClickTypeEnum clickType);
    }
}
