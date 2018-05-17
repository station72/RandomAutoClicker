using System;

namespace RandomAutoClicker.Infrastructure.Events
{
    public class ClickerEventArgs : EventArgs
    {
        public object Sender { get; set; }

        public object Data { get; set; }
    }
}
