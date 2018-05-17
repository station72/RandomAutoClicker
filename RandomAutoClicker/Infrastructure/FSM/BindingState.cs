using RandomAutoClicker.Infrastructure.Events;
using System.Diagnostics;

namespace RandomAutoClicker.Infrastructure.FSM
{
    class BindingState : IState<ClickerEventArgs>
    {
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;

        public BindingState(IEventBroker<ClickerEventArgs> eventBroker)
        {
            _eventBroker = eventBroker;
        }

        public void Handle(ClickerEventArgs arg)
        {
            _eventBroker.Raise(EventNames.KeyPressHandled, new ClickerEventArgs { Data = arg.Data, Sender = this });
        }

        public void OnEnter()
        {
            Debug.Print("BindingState enter");
        }

        public void OnExit()
        {
            Debug.Print("BindingState exit");
        }
    }
}
