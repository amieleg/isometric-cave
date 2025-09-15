using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Drawer
{
    private Texture2D _spriteSheet;

    private Rectangle _bounds;

    public const int PixelSize = 4;
    public const int TileSize = 16;
    public const int ViewDistance = 8;


    private Vector2 _basePoint;
    private World _world;
    private Player _Player;
    private PriorityQueue<Sprite,float> _Entities;
    public Drawer(Rectangle bounds, World world, Player Player)
    {
        _bounds = bounds;
        _basePoint = (new Vector2(_bounds.Width, _bounds.Height) * 0.5f);// - new Vector2(0, ViewDistance * TileSize * PixelSize * 0.5f);
        _world = world;
        _Player = Player;
    }

    public void Load(Texture2D spriteSheet)
    {
        _spriteSheet = spriteSheet;
    }

    public Vector2 ToIsometric(Vector3 Location)
    {
        Vector3 Difference = Location - _Player._Position;
        return _basePoint + (new Vector2(Difference.X * 0.5f - Difference.Y * 0.5f, Difference.X * 0.3125f + Difference.Y * 0.3125f - Difference.Z * 0.4375f) * TileSize * PixelSize);
    }

    public void DrawWorld(SpriteBatch sb, GameTime t)
    {
        for (int z = 0; z < World.WorldHeight; z++)
        {
            for (int y = -ViewDistance; y < ViewDistance; y++)
            {
                for (int x = -ViewDistance; x < ViewDistance; x++)
                {
                    Vector3 TileToDraw = new Vector3(_Player._Position.X + x, _Player._Position.Y + y, z);
                    TileToDraw.Floor();

                    if (TileToDraw.Z >= 0 && TileToDraw.Z < World.WorldHeight && TileToDraw.X >= 0 && TileToDraw.X < World.WorldSize && TileToDraw.Y >= 0 && TileToDraw.Y < World.WorldSize)
                    {
                        Draw(sb, _world.WorldData[(int)TileToDraw.Z, (int)TileToDraw.Y, (int)TileToDraw.X], ToIsometric(TileToDraw + new Vector3(0.5f, 0.5f, 0.5f)), t);
                    }
                }
            }
        }
        Draw(sb, _Player.GetSprite(t), ToIsometric(_Player._Position), t);
    }

    public void Draw(SpriteBatch sb, TileType Type, Vector2 location, GameTime GameTime)
    {
        if (Type != TileType.Empty)
        {
            sb.Draw(
                _spriteSheet,
                location,
                Tile.GetRegion(Type, GameTime),
                Color.White,
                0.0f,
                new Vector2(TileSize / 2.0f, TileSize / 2.0f),
                PixelSize,
                SpriteEffects.None,
                0.0f
                );
        }
    }
    
    public void Draw(SpriteBatch sb, Sprite Sprite, Vector2 location, GameTime GameTime)
    {
        sb.Draw(
            _spriteSheet,
            location,
            Sprite.GetRegion(GameTime),
            Color.White,
            0.0f,
            new Vector2(TileSize / 2.0f, TileSize / 2.0f),
            PixelSize,
            SpriteEffects.None,
            0.0f
            );

    }
}