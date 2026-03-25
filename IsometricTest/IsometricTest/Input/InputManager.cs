

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    KeyboardState _LastState;
    KeyboardState _CurrentState;
    public InputManager()
    {
        _LastState = new KeyboardState();
    }

    public void Update(KeyboardState New)
    {
        _LastState = _CurrentState;
        _CurrentState = New;
    }

    public bool WasPressed(Keys key)
    {
        return (!_LastState.IsKeyDown(key) && _CurrentState.IsKeyDown(key));
    }

    public bool IsDown(Keys key)
    {
        return _CurrentState.IsKeyDown(key);
    }

    public bool WasUnPressed(Keys key)
    {
        return (!_LastState.IsKeyDown(key) && _CurrentState.IsKeyDown(key));
    }
}