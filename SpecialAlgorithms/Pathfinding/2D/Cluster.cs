namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster
    {
        const int CLUSTER_SIZE = 8;
        private GridMap[,] _gridClusters;

        public Cluster(in GridMap gridMap)
        {
            this._gridClusters = new GridMap[SetArraySize(gridMap.YLength), SetArraySize(gridMap.XLength)];
            //DivideGridMapToClusters(gridMap);
        }

        public void DivideGridMapToClusters(in GridMap gridMap)
        {
            int gridX = 0, gridY = 0;
            int clusterX = 0, clusterY = 0;

            while (gridY < gridMap.YLength)
            {
                while (gridX < gridMap.XLength)
                {
                    var clusterGrid = new GridMap(
                        SetClusterSize(gridX, gridMap.XLength),
                        SetClusterSize(gridY, gridMap.YLength)
                    );

                    for (int y = 0; y < clusterGrid.YLength; y++)
                        for (int x = 0; x < clusterGrid.XLength; x++)
                            clusterGrid.SetGridCoordinate(x, y, gridMap.GetGridCoordinate(gridX + x, gridY + y));

                    AddClusterToGrid(clusterX, clusterY, clusterGrid);
                    gridX += clusterGrid.XLength;
                    clusterX++;
                }

                gridX = 0;
                gridY += SetClusterSize(gridY, gridMap.YLength);
                clusterX = 0;
                clusterY++;
            }
        }

        private void AddClusterToGrid(in int xPos, in int yPos, in GridMap clusterGrid)
        {
            this._gridClusters[yPos, xPos] = clusterGrid;
        }

        private int SetArraySize(in int Size)
        {
            int divX = Size / CLUSTER_SIZE;
            int modX = Size % CLUSTER_SIZE;

            return divX + (Size >= CLUSTER_SIZE && modX % CLUSTER_SIZE <= CLUSTER_SIZE / 2 ? 0 : 1);
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

        public bool AllTrue()
        {
            bool ok = false;


            for (int y = 0; y < this._gridClusters.GetLength(0); y++)
            {
                for (int x = 0; x < this._gridClusters.GetLength(1); x++)
                {
                    ok = this._gridClusters[y, x].AllTrue();
                }
            }
            return ok;
        }
    }
}
