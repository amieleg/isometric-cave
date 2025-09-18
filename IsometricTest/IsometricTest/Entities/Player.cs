using System;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.Xna.Framework;

public enum PlayerState
{
    None,
    Nodding,
    Twirling,
    WalkingLeft,
    WalkingRight
}


public class Player
{
    public const float Gravity = 0.2f;
    public Vector3 _Position;
    public World _World;
    public Cuboid _HitBox;
    public Sprite _Sprite = new StaticSprite("kitty");

    public PlayerState _State = PlayerState.None;
    public PlayerState _LastState = PlayerState.None;

    public TimeSpan _StateStart = TimeSpan.Zero;

    public Player(World World)
    {
        _Position = new Vector3(32.5f, 32.5f, 0.5f);
        _HitBox = new Cuboid(0.8f, _Position);
        _World = World;
    }

    public void Move(Vector3 Direction)
    {
        Vector3 NewPosition = _Position + Direction;
        Debug.WriteLine(NewPosition);

        _Position += Direction;
    }

    public void Nod()
    {
        _State = PlayerState.Nodding;
    }

    public void Twirl()
    {
        _State = PlayerState.Twirling;

    }

    public void Nothing()
    {
        _State = PlayerState.None;
        _Sprite = new StaticSprite("kitty");
    }

    public void Update(GameTime Gt)
    {
        bool Switched = _LastState != _State;
        //Debug.WriteLine(_LastState + " " + _State);
        if (Switched)
        {
            _StateStart = Gt.TotalGameTime;
        }
        switch (_State)
        {
            case PlayerState.Nodding:
                if (Switched)
                {
                    _Sprite = new AnimatedSprite("kitty_happy", TimeSpan.FromMilliseconds(200), Gt.TotalGameTime);
                }
                else
                {
                    if (_Sprite.IsDone())
                    {
                        Nothing();
                    }
                }
                break;
            case PlayerState.Twirling:
                if (Switched)
                {
                    _Sprite = new AnimatedSprite("kitty_twirl", TimeSpan.FromMilliseconds(200), Gt.TotalGameTime);
                }
                break;
        }
        _LastState = _State;
    }

    public Sprite GetSprite(GameTime Gt)
    {
        _Sprite.Update(Gt);
        return _Sprite;
    }
}