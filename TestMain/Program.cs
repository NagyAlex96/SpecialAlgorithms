using SpecialAlgorithms.Pathfinding._2D;

namespace TestMain
{
    public class Program
    {
        static void Main(string[] args)
        {

            const int xSize = 1, ySize = 3, totalSize = 1002;
            Cluster cluster;
            bool ok = false;
            DateTime dNow = DateTime.Now;
            for (int i = 0; i < totalSize; i++)
            {
                GridMap gridMap = new GridMap(xSize+i, ySize);

                for (int y = 0; y < gridMap.YLength; y++)
                {
                    for (int x = 0; x < gridMap.XLength; x++)
                    {
                        gridMap.SetGridCoordinate(x, y, true);
                    }
                }
                 cluster = new Cluster(gridMap);

                ok = cluster.AllTrue();
            }


            Console.WriteLine((DateTime.Now - dNow).Seconds);
            Console.WriteLine("Lefutott");
            Console.ReadKey();
        }

    }
}
