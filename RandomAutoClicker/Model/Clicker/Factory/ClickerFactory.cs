using RandomAutoClicker.ViewModel;

namespace RandomAutoClicker.Model.Clicker.Factory
{
    public class ClickerFactory : IClickerFactory
    {
        private readonly IClickerConfigFactory _clickerConfigFactory;
        private readonly IClickBehaviourFactory _clickBehaviourFactory;
        private readonly IClickerIntervalFactory _clickerIntervalFactory;

        public ClickerFactory(
            IClickerConfigFactory clickerConfigFactory,
            IClickBehaviourFactory clickBehaviourFactory,
            IClickerIntervalFactory clickerIntervalFactory
            )
        {
            _clickerConfigFactory = clickerConfigFactory;
            _clickBehaviourFactory = clickBehaviourFactory;
            _clickerIntervalFactory = clickerIntervalFactory;
        }

        public IMouseClicker CreateClicker(ClickDelayEnum clickDelay, ClickAreaEnum clickArea, ClickTypeEnum clickType)
        {
            var clickerConfig = _clickerConfigFactory.CreateClickerConfig(clickArea);
            var clickerBehaviour = _clickBehaviourFactory.CreateClickBehaviour(clickType);
            var clickerInterval = _clickerIntervalFactory.CreateClickerInterval(clickDelay);

            var clicker = new Clicker(clickerInterval, clickerConfig, clickerBehaviour);
            return clicker;
        }
    }
}
