
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite: Sprite
{
    public List<Rectangle> _Regions;
    public TimeSpan _FrameLength;

    public TimeSpan _AnimationStart;
    public int _CurrentRegion;
    public bool _IsDone;

    public AnimatedSprite(String Name, TimeSpan FrameLength, TimeSpan FrameStart)
    {
        _Regions = Atlas.Animations[Name];
        _FrameLength = FrameLength;
        _AnimationStart = FrameStart;
        _CurrentRegion = 0;
    }

    public AnimatedSprite(List<Rectangle> Regions, TimeSpan FrameLength, TimeSpan FrameStart)
    {
        _Regions = Regions;
        _FrameLength = FrameLength;
        _AnimationStart = FrameStart;
        _CurrentRegion = 0;
    }

    public bool IsDone()
    {
        return _IsDone;
    }

    public void Update(GameTime Gt)
    {
        long TotalFrameNumber = (Gt.TotalGameTime.Ticks - _AnimationStart.Ticks) / _FrameLength.Ticks;
        if (TotalFrameNumber >= _Regions.Count)
        {
            _IsDone = true;
        }
        _CurrentRegion = (int)(TotalFrameNumber % _Regions.Count);
    }

    public Rectangle GetRegion()
    {
        return _Regions[_CurrentRegion];
    }
}