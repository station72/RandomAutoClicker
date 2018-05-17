using RandomAutoClicker.Infrastructure.Events;
using System.Diagnostics;

namespace RandomAutoClicker.Infrastructure.FSM
{
    public class IdleState : IState<ClickerEventArgs>
    {
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;

        public IdleState(IEventBroker<ClickerEventArgs> eventBroker)
        {
            _eventBroker = eventBroker;
        }

        public void Handle(ClickerEventArgs arg)
        {
            _eventBroker.Raise(EventNames.ToggleClickerState, new ClickerEventArgs { Data = arg.Data, Sender = this });
        }

        public void OnEnter()
        {
            Debug.Print("IdleState enter");
        }

        public void OnExit()
        {
            Debug.Print("IdleState exit");
        }
    }
}
