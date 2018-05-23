using RandomAutoClicker.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomAutoClicker.Infrastructure.FSM
{
    public class Fsm : IFsm<ClickerEventArgs>
    {
        private readonly List<IState<ClickerEventArgs>> _states;
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;
        private readonly ISubscribesContainer<ClickerEventArgs> _subscribesContainer;
        private IState<ClickerEventArgs> _currentState;

        public Fsm(
            IEventBroker<ClickerEventArgs> eventBroker
            )
        {
            _eventBroker = eventBroker ?? throw new ArgumentNullException(nameof(eventBroker));

            _subscribesContainer = new SubscribesContainer<ClickerEventArgs>(_eventBroker);
            _states = new List<IState<ClickerEventArgs>>();

            InitStates();
            Subscribe();
        }

        private void InitStates()
        {
            AddState(new BindingState(_eventBroker));
            AddState(new IdleState(_eventBroker));

            MoveTo(typeof(IdleState));
        }

        private void Subscribe()
        {
            _subscribesContainer.Subscribe(EventNames.KeyBindingStart, u => MoveTo(typeof(BindingState)));
            _subscribesContainer.Subscribe(EventNames.KeyBindingStop, u => MoveTo(typeof(IdleState)));
            _subscribesContainer.Subscribe(EventNames.KeyPressed, u =>
            {
                CurrentState.Handle(u);
            });
        }

        public void AddState(IState<ClickerEventArgs> state)
        {
            _states.Add(state);
        }

        public IState<ClickerEventArgs> CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        public void MoveTo(Type stateType)
        {
            var state = _states.First(u => u.GetType().Equals(stateType));

            if (CurrentState != null)
                CurrentState.OnExit();

            CurrentState = state;
            CurrentState.OnEnter();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        ~Fsm()
        {
            CleanUp(false);
        }

        private void CleanUp(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                    _subscribesContainer.UnsubscribeAll();
            }

            _disposed = true;
        }
    }
}
