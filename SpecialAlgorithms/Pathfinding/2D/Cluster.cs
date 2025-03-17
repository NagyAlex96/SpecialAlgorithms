using SpecialAlgorithms.Assets;
using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster : ICluster
    {
        [Obsolete("You must not use this!", true)]
        public Cluster()
        {

        }

        public Cluster(int xSize, int ySize)
        {
            ExceptionHelper.ThrowIfArgumentOutOfRange(xSize, 0, Condition.LessOrEqualThan);
            ExceptionHelper.ThrowIfArgumentOutOfRange(ySize, 0, Condition.LessOrEqualThan);

            this.ClusterCollection = new ITile[ySize, xSize];
        }

        public ITile[,] ClusterCollection { get; }
        public List<IPathNode> PathNodes { get; }
        public int XSize => this.ClusterCollection.GetLength(1);
        public int YSize => this.ClusterCollection.GetLength(0);
    }
}
