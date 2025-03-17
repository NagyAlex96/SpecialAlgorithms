using SpecialAlgorithms.Assets;
using SpecialAlgorithms.Pathfinding._2D.Interfaces;


namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Tile : ITile
    {
        [Obsolete("You must not use this!", true)]
        public Tile()
        {

        }

        public Tile(int xPos, int yPos, TerrainType terrainType)
        {
            ExceptionHelper.ThrowIfArgumentOutOfRange(xPos, short.MinValue + 1, short.MaxValue);
            ExceptionHelper.ThrowIfArgumentOutOfRange(yPos, short.MinValue + 1, short.MaxValue);

            this.XPos = (short)xPos;
            this.YPos = (short)yPos;
            this.TerrainType = (byte)terrainType;
        }

        public short XPos { get; }
        public short YPos { get; }
        public byte TerrainType { get; }
        public bool IsConnectionPoint { get; }

        public int HCost { get; set; }
        public int GCost { get; set; }
        public int FCost => GCost + HCost;

        public int CompareTo(ITile? other)
        {
            ExceptionHelper.ThrowIfArgumentIsNull(other, "Null value cannot be compared to something!");

            int comparer = other.FCost.CompareTo(this.FCost);
            if (comparer != 0)
                return comparer;

            comparer = other.GCost.CompareTo(this.GCost);
            if (comparer != 0)
                return comparer;

            return other.HCost.CompareTo(this.HCost);
        }
    }
}
