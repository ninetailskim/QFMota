using System;
using Rotorz.Tile;

namespace Mota
{
    public interface ICommand
    {
        void Execute(int x, int y, TileData otherTileData);
    }
}