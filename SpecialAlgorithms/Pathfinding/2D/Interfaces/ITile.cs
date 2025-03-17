namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface ITile
    {
        public int XPos { get; }
        public int YPos { get; }
        public bool IsConnectionPoint { get; }
        public byte TerrainType { get; }
    }
}
