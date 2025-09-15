using System;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

public class World
{
    public const int WorldSize = 64;
    public const int WorldHeight = 8;
    public TileType[,,] WorldData;

    public World()
    {
        WorldData = new TileType[WorldHeight, WorldSize, WorldSize];
    }

    public void Generate()
    {
        Random rand = new Random();

        for (int z = 0; z < WorldHeight; z++)
        {
            for (int y = 0; y < WorldSize; y++)
            {
                for (int x = 0; x < WorldSize; x++)
                {
                    int tile = rand.Next(0, 5);
                    WorldData[z, y, x] = (TileType)tile;
                }
            }
        }
    }

    public void GenerateOne()
    {
        WorldData[1, 32, 32] = TileType.StoneDirt;
    }

    public bool CanMoveTo(Vector3 Position)
    {
        int X = (int)Position.X;
        int Y = (int)Position.Y;
        int Z = (int)Position.Z;

        if (X < 0 || X >= WorldSize || Y < 0 || Y >= WorldSize || Z < 0 || Z >= WorldHeight)
        {
            return true;
        }

        //Debug.WriteLine(X + " " + Y + " " + Z);

        if (WorldData[Z, Y, X] == TileType.Empty)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}