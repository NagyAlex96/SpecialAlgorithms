using SpecialAlgorithms.Pathfinding._2D;
using SpecialAlgorithms.Pathfinding._2D.Interfaces;

namespace TestMain
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Teszt1();
            const int numForAverage = 1;
            const int multiplier = 16;
            const int xSize = 1024 * multiplier, ySize = 1024 * multiplier;

            ITile[,] tiles = new ITile[ySize, xSize];

            for (int y = 0; y < tiles.GetLength(0); y++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    tiles[y, x] = new Tile(x, y, TerrainType.Ground);
                }
            }


            DateTime dTime;
            List<double> timeValues = new List<double>();

            for (int i = 0; i < numForAverage; i++)
            {
                dTime = DateTime.Now;
                GridMap map = new GridMap(tiles);
                timeValues.Add((DateTime.Now - dTime).TotalSeconds);
            }

            Console.WriteLine($"Clusterekre bontási idő átlaga: {(timeValues.Average())} sec");


            Console.ReadKey();
        }

        private static void Teszt1()
        {
            //const int xSize = 3, ySize = 3;
            //Cluster cluster;
            //bool ok = false;

            //GridMap gridMap = new GridMap(xSize, ySize);

            //for (int y = 0; y < gridMap.YLength; y++)
            //{
            //    for (int x = 0; x < gridMap.XLength; x++)
            //    {
            //        gridMap.SetGridCoordinate(x, y, true);
            //    }
            //}
            //cluster = new Cluster(gridMap);
            //DateTime dNow = DateTime.Now;
            //cluster.DivideGridMapToClusters(gridMap);
            //Console.WriteLine((DateTime.Now - dNow).TotalSeconds);

            //ok = cluster.AllTrue();

            //Console.WriteLine($"OK: {ok}");
            //Console.WriteLine("Lefutott az első");
        }
    }
}
