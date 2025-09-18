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
    private Texture2D _SpriteSheet;

    private Rectangle _Bounds;

    public const int PixelSize = 4;
    public const int TileSize = 16;
    public const int ViewDistance = 8;


    private Vector2 _BasePoint;
    private World _World;
    private Player _Player;

    public Drawer(Rectangle Bounds, World World, Player Player)
    {
        _Bounds = Bounds;
        _BasePoint = (new Vector2(_Bounds.Width, _Bounds.Height) * 0.5f);// - new Vector2(0, ViewDistance * TileSize * PixelSize * 0.5f);
        _World = World;
        _Player = Player;
    }

    public void Load(Texture2D spriteSheet)
    {
        _SpriteSheet = spriteSheet;
    }

    public Vector2 ToIsometric(Vector3 Location)
    {
        Vector3 Difference = Location - _Player._Position;
        return _BasePoint + (new Vector2(Difference.X * 0.5f - Difference.Y * 0.5f, Difference.X * 0.3125f + Difference.Y * 0.3125f - Difference.Z * 0.4375f) * TileSize * PixelSize);
    }

    public void DrawWorld(SpriteBatch Sb, GameTime t)
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
                        DrawTile(Sb, _World.WorldData[(int)TileToDraw.Z, (int)TileToDraw.Y, (int)TileToDraw.X], ToIsometric(TileToDraw), t);
                    }
                }
            }
        }
        Draw(Sb, _Player.GetSprite(t), ToIsometric(_Player._Position), t);
    }

    public void DrawTile(SpriteBatch Sb, TileType Type, Vector2 Location, GameTime Gt)
    {
        if (Type != TileType.Empty)
        {
            Sb.Draw(
                _SpriteSheet,
                Location,
                Atlas.Tiles[Type].GetRegion(),
                Color.White,
                0.0f,
                new Vector2(TileSize / 2.0f , 0),
                PixelSize,
                SpriteEffects.None,
                0.0f
                );
        }
    }
    
    public void Draw(SpriteBatch Sb, Sprite Sprite, Vector2 Location, GameTime Gt)
    {
        Sb.Draw(
            _SpriteSheet,
            Location,
            Sprite.GetRegion(),
            Color.White,
            0.0f,
            new Vector2(TileSize / 2.0f, TileSize / 2.0f),
            PixelSize,
            SpriteEffects.None,
            0.0f
            );

    }
}