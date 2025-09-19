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

public enum TileType
{
    Empty,
    GrassDirt,
    GrassStone,
    Dirt,
    Stone,
    StoneDirt,
}