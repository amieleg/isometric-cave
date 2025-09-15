
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

public class Sprite
{
    public List<Rectangle> _Regions;
    public TimeSpan _FrameLength;

    public TimeSpan _FrameStart;
    public int _CurrentRegion;

    public Sprite(String Name, TimeSpan FrameLength, TimeSpan FrameStart)
    {
        _Regions = Animations[Name];
        _FrameLength = FrameLength;
        _FrameStart = FrameStart;
        _CurrentRegion = 0;
    }

    public Sprite(String Name)
    {
        _Regions = Animations[Name];
        _FrameLength = TimeSpan.Zero;
        _FrameStart = TimeSpan.Zero;
        _CurrentRegion = 0;
    }

    public Rectangle GetRegion()
    {
        return _Regions[0];
    }

    public Rectangle GetRegion(GameTime T)
    {
        if (T.TotalGameTime - _FrameStart < _FrameLength)
        {
            return _Regions[_CurrentRegion];
        }
        _FrameStart = T.TotalGameTime;
        _CurrentRegion = (_CurrentRegion + 1) % _Regions.Count;
        return _Regions[_CurrentRegion];
    }

    public static Rectangle SquareRegion(int X, int Y)
    {
        return new Rectangle(X * Drawer.TileSize, Y * Drawer.TileSize, Drawer.TileSize, Drawer.TileSize);
    }

    public static Rectangle AlignedRegion(int X, int Y, int Width, int Height)
    {
        return new Rectangle(X * Drawer.TileSize, Y * Drawer.TileSize, Width, Height);
    }

    public static Dictionary<String, List<Rectangle>> Animations = new Dictionary<string, List<Rectangle>>();

    public static void InitAnimations()
    {
        Animations.Add(
            "monster",
            [
                SquareRegion(2,0),
                SquareRegion(3,0),
                SquareRegion(2,1),
                SquareRegion(3,1)
            ]
        );

        Animations.Add(
            "guy",
            [
                AlignedRegion(0,2,11,19)
            ]
        );

        Animations.Add(
            "guy_walking",
            [
                AlignedRegion(1,2,11,19),
                AlignedRegion(2,2,11,19)
            ]
        );

        Animations.Add(
            "grass_block",
            [
                SquareRegion(0,0),
                SquareRegion(1,0)
            ]
        );

        Animations.Add(
            "kitty",
            [
                SquareRegion(4,1),
            ]
        );

        Animations.Add(
            "kitty_twirl",
            [
                SquareRegion(5,1),
                SquareRegion(6,1),
                SquareRegion(7,1),
                SquareRegion(4,2),
                SquareRegion(5,2),
            ]
        );

        Animations.Add(
            "kitty_happy",
            [
                SquareRegion(6,3),
                SquareRegion(7,3),
                SquareRegion(6,3)
            ]
        );

        Animations.Add(
            "kitty_walk",
            [
                SquareRegion(4,3),
                SquareRegion(5,3)
            ]
        );
    }
}