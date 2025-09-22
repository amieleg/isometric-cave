using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity
{
    public Vector3 _Position;
    public Cuboid _HitBox;
    public World _World;

    public Entity(Vector3 Position, Cuboid HitBox, World World)
    {
        _Position = Position;
        _HitBox = HitBox;
        _World = World;
    }

    public Entity(World World)
    {
        _World = World;
        _Position = Vector3.Zero;
        _HitBox = new Cuboid(0.0f, 0.0f, 0.0f, _Position);
    }

    public abstract Sprite GetSprite(GameTime Gt);

    public abstract void Update(GameTime Gt);

    public virtual SpriteEffects GetEffects()
    {
        return SpriteEffects.None;
    }
}