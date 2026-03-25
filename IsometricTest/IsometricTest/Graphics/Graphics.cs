using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Drawer
{
    private Texture2D _SpriteSheet;

    private Rectangle _Bounds;

    public const int PixelSize = 8;
    public const int TileSize = 16;
    public const int ViewDistance = 8;
    public const int ViewSize = ViewDistance * 2;


    private Vector2 _BasePoint;
    private World _World;
    private Player _Player;
    private int[,,] _RoughPositions = new int[World.WorldHeight, ViewDistance * 2, ViewDistance * 2];

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
        return _BasePoint + (new Vector2(Difference.X * 0.5f - Difference.Y * 0.5f, Difference.X * 0.25f + Difference.Y * 0.25f - Difference.Z * 0.4375f) * TileSize * PixelSize);
    }

    public (int, int, int) VectorToInt(Vector3 Vector)
    {
        return ((int)Vector.Z, (int)Vector.Y, (int)Vector.X);
    }

    public void DrawCuboid(SpriteBatch Sb, CuboidArea CA, GameTime Gt)
    {
        (int, int, int) StartPoint = CA._StartPoint;
        (int, int, int) EndPoint = CA._EndPoint;
        for (int z = StartPoint.Item3; z <= EndPoint.Item3; z++)
        {
            for (int y = StartPoint.Item2; y <= EndPoint.Item2; y++)
            {
                for (int x = StartPoint.Item1; x <= EndPoint.Item1; x++)
                {
                    Vector3 TileToDraw = new Vector3(x - ViewDistance + _Player._Position.X, y - ViewDistance + _Player._Position.Y, z);
                    TileToDraw.Floor();

                    if (TileToDraw.Z >= 0 && TileToDraw.Z < World.WorldHeight && TileToDraw.X >= 0 && TileToDraw.X < World.WorldSize && TileToDraw.Y >= 0 && TileToDraw.Y < World.WorldSize)
                    {
                        DrawTile(Sb, _World._WorldData[(int)TileToDraw.Z, (int)TileToDraw.Y, (int)TileToDraw.X], ToIsometric(TileToDraw + new Vector3(0.5f, 0.5f, 0.5f)), Gt);
                    }
                }
            }
        }
    }

    public (int, int, int) ToViewDistanceCoords(Vector3 Coords)
    {
        Coords.Floor();
        Vector3 RelativeCoords = Coords - _Player._Position;
        return ((int)RelativeCoords.X + ViewDistance, (int)RelativeCoords.Y + ViewDistance, (int)Coords.Z);
    }

    public CuboidArea[] SplitCuboid(CuboidArea CA, (int, int, int) At)
    {
        return
        [
            new CuboidArea(CA._StartPoint,(CA._EndPoint.Item1, CA._EndPoint.Item2, At.Item3-1)),
            new CuboidArea((CA._StartPoint.Item1, CA._StartPoint.Item2, At.Item3),(CA._EndPoint.Item1, At.Item2-1, CA._EndPoint.Item3)),
            new CuboidArea((CA._StartPoint.Item1, At.Item2, At.Item3),(At.Item1-1, CA._EndPoint.Item2,CA._EndPoint.Item3)),
            new CuboidArea(At,(CA._EndPoint))
        ];
    }

    public int DirectionComparedTo(Vector3 Other, Vector3 To)
    {
        if (Other.Z < To.Z)
        {
            return 0;
        }
        else if (Other.Y < To.Z)
        {
            return 1;
        }
        else if (Other.X < To.Z)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void DrawArea(SpriteBatch Sb, CuboidArea CA, List<Entity> Entities, GameTime Gt)
    {
        if (Entities.Count == 0)
        {
            DrawCuboid(Sb, CA, Gt);
        }
        else if (Entities.Count == 1)
        {
            (int, int, int) EntityCoords = ToViewDistanceCoords(Entities[0]._HitBox.BackLeftDown());

            CuboidArea[] DrawAreas = SplitCuboid(CA, EntityCoords);

            DrawCuboid(Sb, DrawAreas[0], Gt);
            DrawCuboid(Sb, DrawAreas[1], Gt);
            DrawCuboid(Sb, DrawAreas[2], Gt);
            Draw(Sb, Entities[0].GetSprite(Gt), Entities[0].GetEffects(), ToIsometric(Entities[0]._Position), Gt);
            DrawCuboid(Sb, DrawAreas[3], Gt);
        }
        else
        {
            (int, int, int) EntityCoords = ToViewDistanceCoords(Entities[0]._HitBox.BackLeftDown());

            CuboidArea[] DrawAreas = SplitCuboid(CA, EntityCoords);
            List<Entity>[] EntityLists = [new List<Entity>(), new List<Entity>(), new List<Entity>(), new List<Entity>()];

            for (int i = 1; i < Entities.Count; i++)
            {
                Entity E = Entities[i];
                int Direction = DirectionComparedTo(E._HitBox.BackLeftDown(), Entities[0]._HitBox.BackLeftDown());

                EntityLists[Direction].Add(E);
            }

            DrawArea(Sb, DrawAreas[0], EntityLists[0], Gt);
            DrawArea(Sb, DrawAreas[1], EntityLists[1], Gt);
            DrawArea(Sb, DrawAreas[2], EntityLists[2], Gt);
            Draw(Sb, Entities[0].GetSprite(Gt), Entities[0].GetEffects(), ToIsometric(Entities[0]._Position), Gt);
            DrawArea(Sb, DrawAreas[3], EntityLists[3], Gt);
        }
    }

    public void DrawWorld(SpriteBatch Sb, GameTime Gt)
    {
        DrawArea(Sb, new CuboidArea((0, 0, 0), (ViewSize - 1, ViewSize - 1, World.WorldHeight - 1)), _World._Entities, Gt);
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
                new Vector2(TileSize / 2.0f , TileSize / 2.0f),
                PixelSize,
                SpriteEffects.None,
                0.0f
                );
        }
    }
    
    public void Draw(SpriteBatch Sb, Sprite Sprite, SpriteEffects Effects, Vector2 Location, GameTime Gt)
    {
        Sb.Draw(
            _SpriteSheet,
            Location,
            Sprite.GetRegion(),
            Color.White,
            0.0f,
            new Vector2(TileSize / 2.0f, TileSize / 2.0f),
            PixelSize,
            Effects,
            0.0f
            );

    }
}