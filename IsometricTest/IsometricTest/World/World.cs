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

        for (int z = 0; z < WorldHeight / 2.0f; z++)
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

    public void GenerateCycle()
    {
        int i = 0;
        for (int z = 0; z < WorldHeight / 2.0f; z++)
        {
            for (int y = 0; y < WorldSize; y++)
            {
                for (int x = 0; x < WorldSize; x++)
                {
                    i = (i + 1) % 6;

                    WorldData[z, y, x] = (TileType)i;
                }
            }
        }
    }

    public void GenerateOne()
    {
        WorldData[2, 32, 32] = TileType.StoneDirt;
    }

    public bool CanMoveTo(Cuboid HitBox, Vector3 Position)
    {
        Vector3 Back = Position - new Vector3(HitBox._Width, HitBox._Height, HitBox._Depth) * 0.5f;
        Vector3 Front = Position + new Vector3(HitBox._Width, HitBox._Height, HitBox._Depth) * 0.5f;
        for (int z = (int)Back.Z; z <= (int)Front.Z; z++)
        {
            for (int y = (int)Back.Y; y <= (int)Front.Y; y++)
            {
                for (int x = (int)Back.X; x <= (int)Front.X; x++)
                {
                    if (z >= 0 && z < WorldHeight && y >= 0 && y < WorldSize && x >= 0 && x < WorldSize)
                    {
                        if (WorldData[z, y, x] != TileType.Empty)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}