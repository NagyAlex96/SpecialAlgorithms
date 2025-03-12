namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface ITile
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public bool IsConnectionPoint {  get; set; }
        public TerrainType TerrainType { get; set; }
    }
}
