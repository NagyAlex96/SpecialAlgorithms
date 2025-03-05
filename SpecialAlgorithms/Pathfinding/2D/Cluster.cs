using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster
    {
        const int CLUSTER_SIZE = 8;
        private GridMap[,] _gridClusters;

        public Cluster(in GridMap gridMap)
        {
            this._gridClusters = new GridMap[SetArraySize(gridMap.YLength), SetArraySize(gridMap.XLength)];
            DivideGridMapToClusters(gridMap);
        }

        private void DivideGridMapToClusters(in GridMap gridMap)
        {
            int yCoordinate = 0, xCoordinate = 0; //Iterators just like i, j in for-loop
            int clusterXPos = 0, clusterYPos = 0; //where to insert the current cluster 
            int gridCoordinateX = 0, gridCoordinateY = 0; //Iteratos like the recently one, but it help if there is more than 1 cluster
            GridMap clusterGrid = new GridMap(SetClusterSize(xCoordinate, gridMap.XLength), SetClusterSize(yCoordinate, gridMap.YLength));

            while (gridCoordinateY < gridMap.YLength && gridCoordinateX < gridMap.XLength)
            {
                do
                {
                    clusterGrid.SetGridCoordinate(xCoordinate, yCoordinate, gridMap.GetGridCoordinate(gridCoordinateX + xCoordinate, gridCoordinateY + yCoordinate));
                    xCoordinate++;
                } while (gridCoordinateX + xCoordinate < gridMap.XLength && xCoordinate % clusterGrid.XLength != 0);
                yCoordinate++;

                if (xCoordinate % clusterGrid.XLength == 0 && yCoordinate % clusterGrid.YLength != 0)
                {
                    xCoordinate = 0;
                }
                else if (xCoordinate % clusterGrid.XLength == 0 && yCoordinate % clusterGrid.YLength == 0)
                {
                    AddClusterToGrid(clusterXPos++, clusterYPos, clusterGrid);

                    yCoordinate = 0;
                    xCoordinate = 0;
                    gridCoordinateX += xCoordinate + clusterGrid.XLength;
                    if (gridCoordinateX < gridMap.XLength)
                    {
                        clusterGrid = new GridMap(SetClusterSize(gridCoordinateX, gridMap.XLength), SetClusterSize(gridCoordinateY, gridMap.YLength));
                    }
                }
            }
            ;
        }

        private void CheckXCoordinate(ref int xCoordinate, ref int yCoordinate, ref int clusterXPos, ref int clusterYPos, ref int gridCoordinateX, ref int gridCoordinateY, in int gridMapXLenght, in int gridMapYLenght, ref GridMap clusterGrid)
        {
            if (xCoordinate % clusterGrid.XLength == 0 && yCoordinate % clusterGrid.YLength != 0)
            {
                xCoordinate = 0;
            }
            else if (xCoordinate % clusterGrid.XLength == 0 && yCoordinate % clusterGrid.YLength == 0)
            {
                AddClusterToGrid(clusterXPos++, clusterYPos, clusterGrid);

                yCoordinate = 0;
                xCoordinate = 0;
                gridCoordinateX += xCoordinate + clusterGrid.XLength;
                if (gridCoordinateX < gridMapXLenght)
                {
                    clusterGrid = new GridMap(SetClusterSize(gridCoordinateX, gridMapXLenght), SetClusterSize(gridCoordinateY, gridMapYLenght));
                }
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
            bool allTrue = false;
            foreach (var x in _gridClusters)
            {
                allTrue = x.AllTrue();
            }

            return allTrue;
        }
    }
}
