using RandomAutoClicker.Model.Clicker.ClickBehaviour;
using System;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickBehaviourFactory : IClickBehaviourFactory
    {
        public ClickBehaviourBase CreateClickBehaviour(ClickTypeEnum clickType)
        {
            switch (clickType)
            {
                case ClickTypeEnum.None:
                    return new EmptyClickBehaviour();
                case ClickTypeEnum.LeftClick:
                    return new LeftButtonClickBehaviour();
                case ClickTypeEnum.RightClick:
                    return new RightButtonClickBehaviour();
                case ClickTypeEnum.LeftDoubleClick:
                    return new DoubleClickBehaviour(new LeftButtonClickBehaviour());
                case ClickTypeEnum.RightDoubleClick:
                    return new DoubleClickBehaviour(new RightButtonClickBehaviour());
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
