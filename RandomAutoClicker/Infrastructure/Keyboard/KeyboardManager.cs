using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.Infrastructure.FSM;
using System.Runtime.InteropServices;

namespace RandomAutoClicker.Infrastructure.Keyboard
{
    public class KeyboardManager : IKeyboardManager
    {
        //TODO: check hash code ef event brokers
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;
        private readonly IFsm<ClickerEventArgs> _fsm;
        private readonly IKeyboardListener _keyboardListener;

        //private GCHandle _hnd;
        private readonly RawKeyEventHandler _handler;

        public KeyboardManager(
            IEventBroker<ClickerEventArgs> eventBroker,
            IFsm<ClickerEventArgs> fsm,
            IKeyboardListener keyboardListener
            )
        {
            _eventBroker = eventBroker;
            _fsm = fsm;
            _keyboardListener = keyboardListener;

            _handler = new RawKeyEventHandler(OnKeyDown);

            //_hnd = GCHandle.Alloc(_keyboardListener);
        }


        //TODO: invoke multiple times and unsubscribe once. Check how much delegates will remain subscribed
        public void StartHandleKeys()
        {
            _keyboardListener.KeyDown += _handler;
        }

        public void StopHandleKeys()
        {
            _keyboardListener.KeyDown -= _handler;
        }

        void OnKeyDown(object sender, RawKeyEventArgs args)
        {
            _eventBroker.Raise(EventNames.KeyPressed, new ClickerEventArgs
            {
                Data = args.Key,
                Sender = this
            });
        }

        public void Dispose()
        {
            StopHandleKeys();
            //_hnd.Free();
        }
    }
}
