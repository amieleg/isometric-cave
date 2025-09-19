using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum PlayerState
{
    None,
    Nodding,
    Twirling,
    WalkingLeft,
    WalkingRight
}


public class Player : Entity
{
    public Vector3 _Position;
    public World _World;
    public Cuboid _HitBox;
    public Sprite _Sprite = new StaticSprite("kitty");
    public SpriteEffects _Effects = SpriteEffects.None;
    public StateManager<PlayerState, Player> _Sm;

    private static StateTransitionDefinition<PlayerState, Player> _Def;

    static Player()
    {
        _Def = new StateTransitionDefinition<PlayerState, Player>();
        _Def.AddToAction(PlayerState.Nodding, (pl, gt) => pl._Sprite = new AnimatedSprite("kitty_happy", TimeSpan.FromMilliseconds(200), gt.TotalGameTime));
        _Def.AddToAction(PlayerState.None, (pl, gt) => pl._Sprite = new StaticSprite("kitty"));
        _Def.AddToAction(PlayerState.Twirling, (pl, gt) => pl._Sprite = new AnimatedSprite("kitty_twirl", TimeSpan.FromMilliseconds(200), gt.TotalGameTime));
        _Def.AddToAction(PlayerState.WalkingLeft, (pl, gt) => { pl._Sprite = new AnimatedSprite("kitty_walk", TimeSpan.FromMilliseconds(100), gt.TotalGameTime); pl._Effects = SpriteEffects.None; });
        _Def.AddToAction(PlayerState.WalkingRight, (pl, gt) => { pl._Sprite = new AnimatedSprite("kitty_walk", TimeSpan.FromMilliseconds(100), gt.TotalGameTime); pl._Effects = SpriteEffects.FlipHorizontally; });
        _Def.AddStayAction(PlayerState.Nodding, (pl, gt) => { if (pl._Sprite.IsDone()) { pl._Sm.SetState(PlayerState.None); } });
    }


    public Player(World World)
    {
        _Position = new Vector3(32.5f, 32.5f, 0.5f);
        _HitBox = new Cuboid(0.8f, _Position);
        _World = World;
        _Sm = new StateManager<PlayerState, Player>(_Def, this, PlayerState.None, PlayerState.None);
    }


    public void Move(Vector3 Direction)
    {
        Vector3 NewPosition = _Position + Direction;

        if (Direction.X > 0 || Direction.Y < 0)
        {
            _Sm.SetState(PlayerState.WalkingRight);
        }
        else if (Direction.X < 0 || Direction.Y > 0)
        {
            _Sm.SetState(PlayerState.WalkingLeft);
        }
        else if (_Sm._CurrentState == PlayerState.WalkingLeft || _Sm._CurrentState == PlayerState.WalkingRight)
        {
            _Sm.SetState(PlayerState.None);
        }

        _Position += Direction;
    }

    public void Update(GameTime Gt)
    {
        _Sm.Update(Gt);
    }

    public Sprite GetSprite(GameTime Gt)
    {
        _Sprite.Update(Gt);
        return _Sprite;
    }
}