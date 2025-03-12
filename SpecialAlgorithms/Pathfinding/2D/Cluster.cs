using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster : ICluster
    {
        public ITile[,] ClusterCollection { get; }

        public int XSize => this.ClusterCollection.GetLength(1);

        public int YSize => this.ClusterCollection.GetLength(0);

        public Cluster(int xSize, int ySize)
        {
            this.ClusterCollection = new ITile[ySize, xSize];
        }
    }
}
