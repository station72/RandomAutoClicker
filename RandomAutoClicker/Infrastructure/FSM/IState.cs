namespace RandomAutoClicker.Infrastructure.FSM
{
    public interface IState<T>
    {
        void OnEnter();
        void Handle(T arg);
        void OnExit();
    }
}
