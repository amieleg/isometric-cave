using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public enum TileType
{
    Empty,
    GrassDirt,
    GrassStone,
    Dirt,
    Stone,
    StoneDirt,
}

public readonly struct Tile
{
    public Tile(List<Rectangle> Regions, TimeSpan FrameLength)
    {
        _Regions = Regions;
        _FrameLength = FrameLength; // Frames per picture of the animation
        _AnimationLength = FrameLength * Regions.Count;
    }

    public Tile(Rectangle Region)
    {
        _Regions = [Region];
        _FrameLength = TimeSpan.Zero;
        _AnimationLength = TimeSpan.Zero;
    }

    public List<Rectangle> _Regions { get; init; }
    public TimeSpan _FrameLength { get; init; }
    public TimeSpan _AnimationLength { get; init; }



    public static Dictionary<TileType, Tile> TileData = new Dictionary<TileType, Tile>();

    public static void InitTileData()
    {
        TileData.Add(TileType.GrassDirt, StaticSquareTile(0, 0));
        TileData.Add(TileType.GrassStone, StaticSquareTile(1, 0));
        TileData.Add(TileType.Dirt, StaticSquareTile(0, 1));
        TileData.Add(TileType.Stone, StaticSquareTile(1, 1));
        TileData.Add(TileType.StoneDirt, new Tile(
            [new Rectangle(0 * Drawer.TileSize, 1 * Drawer.TileSize, Drawer.TileSize, Drawer.TileSize),
            new Rectangle(1 * Drawer.TileSize, 1 * Drawer.TileSize, Drawer.TileSize, Drawer.TileSize)
            ], TimeSpan.FromMilliseconds(100)));
    }

    public static Tile StaticSquareTile(int X, int Y)
    {
        return new Tile(new Rectangle(X * Drawer.TileSize, Y * Drawer.TileSize, Drawer.TileSize, Drawer.TileSize));
    }

    public static Rectangle GetRegion(TileType Type, GameTime Time)
    {
        Tile Tile = TileData[Type];

        if (Tile._FrameLength == TimeSpan.Zero)
        {
            return Tile._Regions[0];
        }
        else
        {
            int CurrentRegion = (int) ((Time.TotalGameTime.Ticks % Tile._AnimationLength.Ticks) / Tile._FrameLength.Ticks);
            return Tile._Regions[CurrentRegion];
        }
    }
}


