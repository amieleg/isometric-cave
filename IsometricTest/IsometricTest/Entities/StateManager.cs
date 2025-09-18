using System;
using Microsoft.Xna.Framework;

public class StateManager<T>
{
    public T _CurrentState;
    public T _OldState;

    public void ChangeState(T NewState)
    {
        _OldState = _CurrentState;
        _CurrentState = NewState;
    }

    public void ActionInit(T State, Action<GameTime> Function)
    {

    }

    public void ActionUpdate(T State, Action<GameTime> Function)
    {

    }

    public void Update(GameTime Gt)
    {
        
    }

}