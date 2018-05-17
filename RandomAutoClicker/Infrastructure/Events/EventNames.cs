namespace RandomAutoClicker.Infrastructure.Events
{
    public class EventNames
    {
        public static readonly string KeyPressed = nameof(KeyPressed);
        public static readonly string KeyBindingStart = nameof(KeyBindingStart);
        public static readonly string KeyBindingStop = nameof(KeyBindingStop);
        public static readonly string KeyPressHandled = nameof(KeyPressHandled);
        public static readonly string ToggleClickerState = nameof(ToggleClickerState);
    }
}
