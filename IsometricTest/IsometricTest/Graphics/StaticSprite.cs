
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StaticSprite : Sprite
{
    public Rectangle _Region;

    public StaticSprite(String Name)
    {
        _Region = Atlas.Sprites[Name];
    }

    public StaticSprite(Rectangle Region)
    {
        _Region = Region;
    }

    public Rectangle GetRegion()
    {
        return _Region;
    }
}