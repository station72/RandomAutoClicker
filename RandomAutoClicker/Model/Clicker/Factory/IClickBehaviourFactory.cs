using RandomAutoClicker.Model.Clicker.ClickBehaviour;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public interface IClickBehaviourFactory
    {
        ClickBehaviourBase CreateClickBehaviour(ClickTypeEnum clickType);
    }
}
