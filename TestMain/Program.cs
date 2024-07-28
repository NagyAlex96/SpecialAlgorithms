using SpecialAlgorithms;
using SpecialAlgorithms.BinaryTree;
using SpecialAlgorithms.LinkedList;
using System.Runtime;

namespace TestMain
{
    public class Program
    {
        static Random rnd = new Random();
        const int size = 250000000;
        const int TestNum = 3;
        static void Main(string[] args)
        {
            LinkedListNormal<int> lancolt = new LinkedListNormal<int>();


            Console.WriteLine("\nLefutott");
            Console.ReadKey();
        }

        static void AtlagKereses()
        {
            LinkedListNormal<int> lancoltLista = new LinkedListNormal<int>();

            for (int i = 0; i < size; i++)
            {
                lancoltLista.InsertAfterTheLastElement(rnd.Next(int.MinValue, int.MaxValue));
            }
            Console.WriteLine("Adatok betöltése kész");

            List<double> time = new List<double>();
            for (int i = 0; i < TestNum; i++)
            {
                DateTime dTime = DateTime.Now;
                var a = lancoltLista.LinearSearch(rnd.Next(int.MinValue, int.MaxValue));
                time.Add((DateTime.Now - dTime).TotalSeconds);
            }

            Console.WriteLine($"{TestNum} lefutás után az átlag, ami kereséssel telt: {time.Average()}\nLegkevesebb idő: {time.Min()}\nLegtöbb idő: {time.Max()}");
        }
    }
}
