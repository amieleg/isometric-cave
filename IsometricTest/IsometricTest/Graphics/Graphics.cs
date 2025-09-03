using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Drawer
{
    private Texture2D _spriteSheet;

    private Rectangle _bounds;

    public const float PixelSize = 4.0f;
    public const float TileSize = 16.0f;
    public const int ViewDistance = 8;
    private Vector2 _basePoint;
    private World _world;
    private Player _player;
    private SpriteAtlas _spriteAtlas;
    public Drawer(Rectangle bounds, World world, Player player)
    {
        _bounds = bounds;
        _basePoint = (new Vector2(_bounds.Width, _bounds.Height) * 0.5f) - new Vector2(0, ViewDistance * TileSize * PixelSize * 0.5f);
        _world = world;
        _player = player;
        _spriteAtlas = new SpriteAtlas();
    }

    public void Load(Texture2D spriteSheet)
    {
        _spriteSheet = spriteSheet;
    }

    public Vector2 ToIsometric(Vector3 location)
    {
        return _basePoint + (new Vector2(location.X * 0.5f - location.Y * 0.5f, location.X * 0.3125f + location.Y * 0.3125f - location.Z * 0.4375f) * TileSize * PixelSize);
    }

    public void DrawWorld(SpriteBatch sb, GameTime t)
    {
        for (int z = 0; z < World.WorldHeight; z++)
        {
            for (int y = - ViewDistance; y < ViewDistance; y++)
            {
                for (int x = - ViewDistance; x < ViewDistance; x++)
                {
                    Vector3 drawSpot = new Vector3(_player._position.X + x, _player._position.Y + y, z);
                    drawSpot.Floor();

                    Vector3 offset = new Vector3(_player._position.X % 1, _player._position.Y % 1, _player._position.Z);


                    if (drawSpot.Z >= 0 && drawSpot.Z < World.WorldHeight && drawSpot.X >= 0 && drawSpot.X < World.WorldSize && drawSpot.Y >= 0 && drawSpot.Y < World.WorldSize)
                    {
                        Draw(sb, _world.WorldData[(int)drawSpot.Z, (int)drawSpot.Y, (int)drawSpot.X], ToIsometric(new Vector3(x + ViewDistance, y + ViewDistance, z) - offset));
                    }
                }
            }
        }
    }

    public void Draw(SpriteBatch sb, int spriteId, Vector2 location)
    {
        Rectangle region = Rectangle.Empty;

        if (spriteId != 0)
        {
            sb.Draw(
                _spriteSheet,
                location,
                _spriteAtlas.GetRegion(spriteId),
                Color.White,
                0.0f,
                new Vector2(TileSize / 2.0f, 0),
                PixelSize,
                SpriteEffects.None,
                0.0f
                );
        }
    }
}