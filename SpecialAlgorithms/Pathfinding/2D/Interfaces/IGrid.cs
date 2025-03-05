namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface IGridMap
    {

        /// <summary>
        /// Grid létrehozása
        /// </summary>
        /// <param name="size">Sorok és oszlopok száma</param>
        public void CreateGrid(int size);

        /// <summary>
        /// Grid létrehozása
        /// </summary>
        /// <param name="xLength">OszlopSzám</param>
        /// <param name="yLength">SorSzám</param>
        public void CreateGrid(int xLength, int yLength);

        /// <summary>
        /// Grid kiürítése
        /// </summary>
        public void Clear();

        /// <summary>
        /// Visszaadja az oszlopok számát
        /// </summary>
        public int XLength { get; }

        /// <summary>
        /// Visszaadja a sorok számát
        /// </summary>
        public int YLength { get; }

        /// <summary>
        /// Egy koordináta beállítása
        /// </summary>
        /// <param name="xCoordinate">OszlopIndex</param>
        /// <param name="yCoordinate">SorIndex</param>
        /// <param name="state">Adott koordinátán található érték</param>
        public void SetGridCoordinate(int xCoordinate, int yCoordinate, bool state);

        /// <summary>
        /// Visszaadja egy koordinátán található értéket
        /// </summary>
        /// <param name="xCoordinate">SorIndex</param>
        /// <param name="yCoordinate">OszlopIndex</param>
        public bool GetGridCoordinate(int xCoordinate, int yCoordinate);

    }
}
