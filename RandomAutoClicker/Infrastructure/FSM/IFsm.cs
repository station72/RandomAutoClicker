using RandomAutoClicker.Infrastructure.Events;
using System;

namespace RandomAutoClicker.Infrastructure.FSM
{
    public interface IFsm<T>
    {
        void AddState(IState<T> state);

        void MoveTo(Type state);

        IState<T> Current();
    }
}
