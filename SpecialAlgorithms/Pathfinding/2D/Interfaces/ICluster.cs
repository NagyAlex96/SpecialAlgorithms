namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface ICluster
    {
        public ITile[,] ClusterCollection { get; }

        public int XSize { get; }
        public int YSize { get; }
    }
}
