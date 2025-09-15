using System;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.Xna.Framework;

public class Player
{
    public const float Gravity = 0.2f;
    public Vector3 _Position;
    public World _World;
    public Sprite _Sprite = new Sprite("kitty");
    public Sprite _Walking;
    public Sprite _Twirling;
    public Sprite _Happy;

    public bool _IsWalkingLeft = false;
    public bool _IsWalkingRight = false;
    public bool _IsTwirling = false;
    public bool _IsNodding = false;

    public Player(World World)
    {
        _Position = new Vector3(32, 32, 10);
        _World = World;
    }

    public void Move(Vector3 Direction)
    {
        Vector3 NewPosition = _Position + Direction;

        if (_World.CanMoveTo(NewPosition))
        {
            if (Direction != Vector3.Zero)
            {
                _IsTwirling = false;
                _IsNodding = false;
            }
            if (Direction.X < 0 || Direction.Y > 0)
            {
                _IsWalkingLeft = true;
            }
            if (Direction.X > 0 || Direction.Y < 0)
            {
                _IsWalkingRight = true;
            }
            _Position += Direction;
        }
    }

    public void Nod(GameTime Gt)
    {
        if (!_IsNodding)
        {
            _IsNodding = true;
            _Happy = new Sprite("kitty_happy", TimeSpan.FromMilliseconds(400), Gt.TotalGameTime);
        }
    }

    public void Twirl(GameTime Gt)
    {
        if (!_IsTwirling)
        {
            _IsTwirling = true;
            _Twirling = new Sprite("kitty_twirl", TimeSpan.FromMilliseconds(200), Gt.TotalGameTime);
        }
    }

    public Sprite GetSprite(GameTime Gt)
    {
        if (_IsNodding)
        {
            return _Happy;
        }
        if (_IsTwirling)
        {
            return _Twirling;
        }
        return _Sprite;
    }
}