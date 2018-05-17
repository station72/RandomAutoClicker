using RandomAutoClicker.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomAutoClicker.Infrastructure.FSM
{
    public class Fsm : IFsm<ClickerEventArgs>
    {
        private readonly List<IState<ClickerEventArgs>> _states;
        private IState<ClickerEventArgs> _currentState;
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;
        private readonly ISubscribesContainer<ClickerEventArgs> _subscribesContainer;

        public Fsm(
            IEventBroker<ClickerEventArgs> eventBroker,
            ISubscribesContainer<ClickerEventArgs> subscribesContainer
            )
        {
            _eventBroker = eventBroker;
            _subscribesContainer = subscribesContainer;

            _states = new List<IState<ClickerEventArgs>>();

            AddState(new BindingState(_eventBroker));
            AddState(new IdleState(_eventBroker));

            MoveTo(typeof(IdleState));

            Init();
        }

        private void Init()
        {
            _subscribesContainer.Subscribe(EventNames.KeyBindingStart, u => MoveTo(typeof(BindingState)));
            _subscribesContainer.Subscribe(EventNames.KeyBindingStop, u => MoveTo(typeof(IdleState)));
            _subscribesContainer.Subscribe(EventNames.KeyPressed, u =>
            {
                Current().Handle(u);
            });
        }

        public void AddState(IState<ClickerEventArgs> state)
        {
            _states.Add(state);
        }

        public IState<ClickerEventArgs> Current()
        {
            return _currentState;
        }

        public void MoveTo(Type stateType)
        {
            var state = _states.First(u => u.GetType().Equals(stateType));

            if (Current() != null)
                Current().OnExit();

            _currentState = state;
            Current().OnEnter();
        }
    }
}
