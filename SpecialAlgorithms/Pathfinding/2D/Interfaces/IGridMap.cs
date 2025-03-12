namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface IGridMap
    {
        public ICluster[,] GridMapCollection { get; }

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
        public void SetGridCoordinate(int xCoordinate, int yCoordinate, ITile[,] state);

        /// <summary>
        /// Visszaadja egy koordinátán található értéket
        /// </summary>
        /// <param name="xCoordinate">SorIndex</param>
        /// <param name="yCoordinate">OszlopIndex</param>
        public ITile[,] GetGridCoordinate(int xCoordinate, int yCoordinate);
    }
}
