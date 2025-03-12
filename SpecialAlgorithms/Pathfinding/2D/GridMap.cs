using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct GridMap : IGridMap
    {
        const int CLUSTER_SIZE = 8;

        public ICluster[,] GridMapCollection { get; private set; }

        public GridMap(ITile[,] tiles)
        {
            this.GridMapCollection = new ICluster[SetArraySize(tiles.GetLength(0)), SetArraySize(tiles.GetLength(1))];
            DivideGridMapToClusters(tiles);
        }

        public int XLength => this.GridMapCollection.GetLength(1);

        public int YLength => this.GridMapCollection.GetLength(0);

        public ITile[,] GetGridCoordinate(int xCoordinate, int yCoordinate)
        {
            throw new NotImplementedException();
            //if (xCoordinate < 0 || xCoordinate >= this.XLength)
            //{
            //    throw new InvalidOperationException("One of the X coordinate is out of the bounds!");
            //}
            //else if (yCoordinate < 0 || yCoordinate >= this.YLength)
            //{
            //    throw new InvalidOperationException("One of the Y coordinate is out of the bounds!");
            //}
            //else
            //{
            //    return this.GridMapCollection[yCoordinate, xCoordinate].ClusterCollection;
            //}
        }

        public void SetGridCoordinate(int xCoordinate, int yCoordinate, ITile[,] state)
        {
            throw new NotImplementedException();

            //if (xCoordinate < 0 || xCoordinate >= this.XLength)
            //{
            //    throw new InvalidOperationException("One of the X coordinate is out of the bounds!");
            //}
            //else if (yCoordinate < 0 || yCoordinate >= this.YLength)
            //{
            //    throw new InvalidOperationException("One of the Y coordinate is out of the bounds!");
            //}
            //else
            //{
            //    this.GridMapCollection[yCoordinate, xCoordinate].SetCluster(state);
            //}
        }

        private void DivideGridMapToClusters(in ITile[,] tileMap)
        {
            int gridX = 0, gridY = 0;
            int clusterX = 0, clusterY = 0;

            while (gridY < tileMap.GetLength(0))
            {
                while (gridX < tileMap.GetLength(1))
                {
                    var clusterGrid = new Cluster(
                        SetClusterSize(gridX, tileMap.GetLength(1)),
                        SetClusterSize(gridY, tileMap.GetLength(1))
                    );

                    for (int y = 0; y < clusterGrid.YSize; y++)
                        for (int x = 0; x < clusterGrid.XSize; x++)
                            clusterGrid.ClusterCollection[y, x] = tileMap[gridY + y, gridX + x];

                    this.GridMapCollection[clusterY, clusterX] = clusterGrid;
                    gridX += clusterGrid.XSize;
                    clusterX++;
                }

                gridX = 0;
                gridY += SetClusterSize(gridY, tileMap.GetLength(0));
                clusterX = 0;
                clusterY++;
            }
        }

        private int SetClusterSize(in int fromCoordinate, in int toCoordinate)
        {
            int xSize = toCoordinate - fromCoordinate;
            if (xSize % CLUSTER_SIZE == 0)
            {
                return CLUSTER_SIZE;
            }
            else if (xSize < CLUSTER_SIZE || (xSize > CLUSTER_SIZE && xSize <= CLUSTER_SIZE + CLUSTER_SIZE / 2))
            {
                return xSize;
            }
            else
            {
                return CLUSTER_SIZE;
            }
        }

        private int SetArraySize(in int Size)
        {
            int divX = Size / CLUSTER_SIZE;
            int modX = Size % CLUSTER_SIZE;

            return divX + (Size >= CLUSTER_SIZE && modX % CLUSTER_SIZE <= CLUSTER_SIZE / 2 ? 0 : 1);
        }
    }
}
