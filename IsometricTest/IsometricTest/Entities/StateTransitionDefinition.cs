
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class StateTransitionDefinition<TState, T> where TState : System.Enum
{
    private Dictionary<TState, Action<T, GameTime>> _ToActions = new Dictionary<TState, Action<T, GameTime>>();
    private Dictionary<TState, Action<T, GameTime>> _StayActions = new Dictionary<TState, Action<T, GameTime>>();
    private Dictionary<(TState, TState), Action<T, GameTime>> _SpecialActions = new Dictionary<(TState, TState), Action<T, GameTime>>();
    private Action<T, GameTime> _EmptyAction = (t, gt) => {};


    public void AddToAction(TState ToState, Action<T, GameTime> Action) => _ToActions.Add(ToState, Action);
    public void AddStayAction(TState ToState, Action<T, GameTime> Action) => _StayActions.Add(ToState, Action);
    public void AddSpecialAction(TState FromState, TState ToState, Action<T, GameTime> Action) => _SpecialActions.Add((FromState, ToState), Action);
    

    public bool TryGetAction(TState FromState, TState ToState, out Action<T, GameTime> OutAction)
    {
        if (FromState.Equals(ToState))
        {
            if (_StayActions.TryGetValue(ToState, out OutAction))
            {
                return true;
            }
            return false;
        }
        else
        {
            if (_SpecialActions.TryGetValue((FromState, ToState), out OutAction))
            {
                return true;
            }
            else if (_ToActions.TryGetValue((ToState), out OutAction))
            {
                return true;
            }
            return false;
        }
    }
}