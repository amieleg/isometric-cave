using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class World
{
    public const int WorldSize = 64;
    public const int WorldHeight = 8;
    public int[,,] WorldData;

    public World()
    {
        WorldData = new int[WorldHeight, WorldSize, WorldSize];
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
                    int tile = rand.Next(0,5);
                    WorldData[z, y, x] = tile;
                }
            }
        }
    }

    public void GenerateOne()
    {
        WorldData[1, 32, 32] = 1;
    }
}