using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Tile : ITile
    {
        [Obsolete("You must not use this!",true)]
        public Tile()
        {
            
        }

        public Tile(int xPos, int yPos, TerrainType terrainType)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.TerrainType = (byte)terrainType;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public byte TerrainType { get; set; }
        public bool IsConnectionPoint { get; set; }
    }
}
