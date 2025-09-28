using Microsoft.Xna.Framework;

public class StaticEntity : Entity
{
    private Sprite _Sprite;
    public StaticEntity(Sprite Sprite, Vector3 Position, World World) : base(Position, new Cuboid(1.0f, Position), World)
    {
        _Sprite = Sprite;
    }

    public override void Update(GameTime Gt)
    {
        _Sprite.Update(Gt);
    }

    public override Sprite GetSprite(GameTime Gt)
    {
        return _Sprite;
    }
}