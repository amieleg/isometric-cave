using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public interface Sprite
{
    public Rectangle GetRegion();

    public void Update(GameTime Gt)
    {

    }

    public bool IsDone()
    {
        return true;
    }
}