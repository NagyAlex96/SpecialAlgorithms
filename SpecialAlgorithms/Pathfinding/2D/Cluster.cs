using SpecialAlgorithms.Pathfinding._2D.Interfaces;
using System;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct Cluster
    {
    {
        const int CLUSTER_SIZE = 8;
        private GridMap[,] _gridClusters;

        public Cluster(in GridMap gridMap)
        {
            this._gridClusters = new GridMap[SetArraySize(gridMap.YLength), SetArraySize(gridMap.XLength)];
            //DivideGridMapToClusters(gridMap);
        }

        public void DivideGridMapToClusters2(in GridMap gridMap)
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

        public void DivideGridMapToClusters(in GridMap gridMap)
        {
            int yCoordinate = 0, xCoordinate = 0;
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
                    if (gridCoordinateX < gridMap.XLength && clusterXPos < _gridClusters.GetLength(1))
                    {
                        gridCoordinateX += clusterGrid.XLength;
                        AddClusterToGrid(clusterXPos++, clusterYPos, clusterGrid);
                    }
                    else if (gridCoordinateY < gridMap.YLength && clusterYPos + 1 < _gridClusters.GetLength(0))
                    {
                        gridCoordinateY += clusterGrid.YLength;
                        AddClusterToGrid(clusterXPos, clusterYPos++, clusterGrid);
                    }
                    else
                    {
                        AddClusterToGrid(clusterXPos, clusterYPos, clusterGrid);
                        break;
                    }
                    xCoordinate = 0;
                    yCoordinate = 0;

                    if (gridCoordinateX < gridMap.XLength)
                    {
                        clusterGrid = new GridMap(SetClusterSize(gridCoordinateX, gridMap.XLength), SetClusterSize(gridCoordinateY, gridMap.YLength));
                    }
                    else if (gridCoordinateY < gridMap.YLength)
                    {
                        gridCoordinateY += clusterGrid.YLength;
                        if (gridCoordinateX == gridMap.XLength && gridCoordinateY != gridMap.YLength)
                        {
                            gridCoordinateX = 0;
                            clusterXPos = 0;
                            clusterYPos++;
                            clusterGrid = new GridMap(SetClusterSize(gridCoordinateX, gridMap.XLength), SetClusterSize(gridCoordinateY, gridMap.YLength));
                        }
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
