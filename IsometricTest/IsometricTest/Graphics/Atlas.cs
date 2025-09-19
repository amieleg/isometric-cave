using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Atlas
{
    public static void InitAtlas()
    {
        InitSprites();
        InitAnimations();
        InitTiles();
    }

    public static Rectangle SquareRegion(int X, int Y)
    {
        return new Rectangle(X * Drawer.TileSize, Y * Drawer.TileSize, Drawer.TileSize, Drawer.TileSize);
    }

    public static Rectangle AlignedRegion(int X, int Y, int Width, int Height)
    {
        return new Rectangle(X * Drawer.TileSize, Y * Drawer.TileSize, Width, Height);
    }

    public static Dictionary<String, Rectangle> Sprites = new Dictionary<string, Rectangle>();

    public static void InitSprites()
    {
        Sprites.Add(
            "kitty",
            SquareRegion(4, 1)
        );
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

    public static Dictionary<TileType, Sprite> Tiles = new Dictionary<TileType, Sprite>();

    public static void InitTiles()
    {
        Tiles.Add(TileType.GrassDirt, new StaticSprite(SquareRegion(0, 1)));
        Tiles.Add(TileType.GrassStone, new StaticSprite(SquareRegion(1, 0)));
        Tiles.Add(TileType.Dirt, new StaticSprite(SquareRegion(0, 1)));
        Tiles.Add(TileType.Stone, new StaticSprite(SquareRegion(1, 1)));
        Tiles.Add(TileType.StoneDirt, new AnimatedSprite([
            SquareRegion(0,1),
            SquareRegion(1,1)
            ], TimeSpan.FromMilliseconds(250), TimeSpan.Zero
            ));
    }

    public static void UpdateTiles(GameTime Gt)
    {
        foreach (Sprite Tile in Tiles.Values)
        {
            Tile.Update(Gt);
        }
    }
}