using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Tile : ITile
    {
        public Tile(int xPos, int yPos, TerrainType terrainType)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.TerrainType = terrainType;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public TerrainType TerrainType { get; set; }
        public bool IsConnectionPoint { get; set; }
    }
}
