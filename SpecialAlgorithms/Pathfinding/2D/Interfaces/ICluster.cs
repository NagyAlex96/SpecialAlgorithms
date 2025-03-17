namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface ICluster
    {
        public ITile[,] ClusterCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<IPathNode> PathNodes { get; }

        /// <summary>
        /// Horizontal size
        /// </summary>
        public int XSize { get; }

        /// <summary>
        /// Vertical size
        /// </summary>
        public int YSize { get; }
    }
}
