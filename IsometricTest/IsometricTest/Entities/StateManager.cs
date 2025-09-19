using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StateManager<TState, T> where TState : System.Enum
{
    public TState _LastState;
    public TState _CurrentState;
    public T _Entity;

    public StateTransitionDefinition<TState, T> _Definition;

    public StateManager(StateTransitionDefinition<TState, T> Definition, T Entity, TState StartState1, TState StartState2)
    {
        _LastState = StartState1;
        _CurrentState = StartState2;
        _Entity = Entity;
        _Definition = Definition;
    }


    public void SetState(TState NewState)
    {
        _CurrentState = NewState;
    }

    public void Update(GameTime Gt)
    {
        Action<T, GameTime> Action;
        if (_Definition.TryGetAction(_LastState, _CurrentState, out Action))
        {
            _LastState = _CurrentState;
            Action.Invoke(_Entity, Gt);
        }
        else
        {
            _LastState = _CurrentState;
        }
    }
}