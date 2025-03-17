namespace SpecialAlgorithms.Pathfinding._2D.Interfaces
{
    public interface ITile : IComparable<ITile>
    {
        /// <summary>
        /// X-Coordinate
        /// </summary>
        public short XPos { get; }

        /// <summary>
        /// Y-Coordinate
        /// </summary>
        public short YPos { get; }

        public bool IsConnectionPoint { get; }

        public byte TerrainType { get; }

        /// <summary>
        /// Heuristic Cost
        /// </summary>
        public int HCost { get; set; }

        /// <summary>
        /// Walking Cost
        /// </summary>
        public int GCost { get; set; }

        /// <summary>
        /// Summa of Heuristic and Walking Cost
        /// </summary>
        public int FCost { get; }
    }
}
