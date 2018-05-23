using System;

namespace RandomAutoClicker.Infrastructure.FSM
{
    public interface IFsm<T>: IDisposable
    {
        void AddState(IState<T> state);

        void MoveTo(Type state);

        IState<T> CurrentState { get; }
    }
}
