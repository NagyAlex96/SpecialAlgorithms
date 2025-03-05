using SpecialAlgorithms.Pathfinding._2D.Interfaces;
using System.ComponentModel.Design;
using System.Security.Principal;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster
    {
        const int CLUSTER_SIZE = 8;
        private GridMap[,] _gridClusters;

        public Cluster(in GridMap gridMap)
        {
            this._gridClusters = new GridMap[SetArrayYSize(gridMap.YLength), SetArrayXSize(gridMap.XLength)];
            DivideGridMapToClusters(gridMap);
        }

        private void DivideGridMapToClusters(in GridMap gridMap)
        {
            int yCoordinate = 0, xCoordinate = 0;
            int clusterXPos = 0, clusterYPos = 0;
            int gridCoordinateX = 0, gridCoordinateY = 0;
            GridMap clusterGrid = new GridMap(SetClusterXSize(xCoordinate, gridMap.XLength), SetClusterYSize(yCoordinate, gridMap.YLength));

            while (gridCoordinateY < gridMap.YLength && gridCoordinateX < gridMap.XLength) //15x3
            {
                do
                {
                    clusterGrid.SetGridCoordinate(xCoordinate, yCoordinate, gridMap.GetGridCoordinate(gridCoordinateX + xCoordinate, gridCoordinateY + yCoordinate));
                    xCoordinate++;
                } while (gridCoordinateX + xCoordinate < gridMap.XLength && xCoordinate % CLUSTER_SIZE != 0);
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
                    gridCoordinateX = clusterXPos * CLUSTER_SIZE + xCoordinate;
                    if (gridCoordinateX < gridMap.XLength)
                    {
                        clusterGrid = new GridMap(SetClusterXSize(gridCoordinateX, gridMap.XLength), SetClusterYSize(gridCoordinateY, gridMap.YLength));
                    }
                }
            }
            ;
        }

        private void AddClusterToGrid(in int xPos, in int yPos, in GridMap clusterGrid)
        {
            this._gridClusters[yPos, xPos] = clusterGrid;
        }

        private int SetArrayXSize(in int xSize)
        {
            int divX = xSize / CLUSTER_SIZE;
            int modX = xSize % CLUSTER_SIZE;

            return divX + (xSize >= CLUSTER_SIZE && modX % CLUSTER_SIZE <= CLUSTER_SIZE / 2 ? 0 : 1);
        }

        private int SetArrayYSize(in int ySize)
        {
            int divY = ySize / CLUSTER_SIZE;
            int modY = ySize % CLUSTER_SIZE;

            return divY + (ySize >= CLUSTER_SIZE && modY % CLUSTER_SIZE <= CLUSTER_SIZE / 2 ? 0 : 1);
        }

        private int SetClusterXSize(in int fromX, in int toX)
        {
            int xSize = toX - fromX;
            if (xSize % CLUSTER_SIZE == 0)
            {
                return CLUSTER_SIZE;
            }
            else if (xSize > CLUSTER_SIZE && xSize % CLUSTER_SIZE <= CLUSTER_SIZE / 2)
            {
                return CLUSTER_SIZE + CLUSTER_SIZE / 2;
            }
            else if (xSize < CLUSTER_SIZE)
            {
                return xSize;
            }
            else
            {
                return CLUSTER_SIZE;
            }
        }

        private int SetClusterYSize(in int fromY, in int toY)
        {
            int ySize = toY - fromY;
            if (ySize % CLUSTER_SIZE == 0)
            {
                return CLUSTER_SIZE;
            }
            else if (ySize > CLUSTER_SIZE && ySize % CLUSTER_SIZE <= CLUSTER_SIZE / 2)
            {
                return CLUSTER_SIZE + CLUSTER_SIZE / 2;
            }
            else if (ySize < CLUSTER_SIZE)
            {
                return ySize;
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
