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

public class SpriteAtlas
{
    private const int SheetWidth = 4;
    private const int SheetHeight = 2;

    private Dictionary<int, Rectangle> _textures;
    private Dictionary<int, List<Rectangle>> _animations;

    public SpriteAtlas()
    {
        _textures = new Dictionary<int, Rectangle>();
        _animations = new Dictionary<int, List<Rectangle>>();

        _textures.Add(1, GetRectangleFromNumber(0));
        _textures.Add(2, GetRectangleFromNumber(1));
        _textures.Add(3, GetRectangleFromNumber(4));
        _textures.Add(4, GetRectangleFromNumber(5));

        List<Rectangle> animation1 =
        [
            GetRectangleFromNumber(2),
            GetRectangleFromNumber(3),
            GetRectangleFromNumber(6),
            GetRectangleFromNumber(7),
        ];

        _animations.Add(1, animation1);
    }

    private static Rectangle GetRectangleFromNumber(int n)
    {
        return new Rectangle((int)((n % SheetWidth) * Drawer.TileSize), (int)((n / SheetWidth) * Drawer.TileSize), (int)Drawer.TileSize, (int)Drawer.TileSize);
    }

    public Rectangle GetRegion(int id)
    {
        return _textures[id];
    }

    /*public Rectangle GetAnimationRegion(Animation animation, GameTime t)
    {
        TimeSpan elapsed = t.ElapsedGameTime - animation._startTime;

        int framn = (int) elapsed / TimeSpan.FromMilliseconds(100);
    }*/
}