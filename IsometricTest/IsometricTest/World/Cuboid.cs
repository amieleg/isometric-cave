using System;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.Xna.Framework;

public class Cuboid
{
    public float _Width;
    public float _Height;
    public float _Depth;

    public Vector3 _Location;

    public Cuboid(float Width, float Height, float Depth, Vector3 Location)
    {
        _Width = Width;
        _Height = Height;
        _Depth = Depth;
        _Location = Location; // Centre
    }

    public Cuboid(float Size, Vector3 Location)
    {
        _Width = Size;
        _Height = Size;
        _Depth = Size;
        _Location = Location;
    }

    public Cuboid(float Width, float Height, float Depth)
    {
        _Width = Width;
        _Height = Height;
        _Depth = Depth;
        _Location = Vector3.Zero;
    }

    public Cuboid(float Size)
    {
        _Width = Size;
        _Height = Size;
        _Depth = Size;
        _Location = Vector3.Zero;
    }

    public bool Collides(Cuboid Other)
    {
        return false;
    }
}