using SpecialAlgorithms.Pathfinding._2D.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAlgorithms.Pathfinding._2D
{
    public struct GridMap
    {
        private bool[,] _tileCollection;

        public GridMap()
        {

        }

        public GridMap(int size)
        {
            this._tileCollection = new bool[size, size];
        }

        public GridMap(int xLength, int yLength)
        {
            this._tileCollection = new bool[yLength, xLength];
        }

        public int YLength => this._tileCollection.GetLength(0);
        public int XLength => this._tileCollection.GetLength(1);

        public void CreateGrid(int size)
        {
            this._tileCollection = new bool[size, size];
        }

        public void CreateGrid(int xLength, int yLength)
        {
            this._tileCollection = new bool[yLength, xLength];
        }

        public bool GetGridCoordinate(int xCoordinate, int yCoordinate)
        {
            if (IsOutOfBounds(xCoordinate, yCoordinate))
            {
                throw new InvalidOperationException("One of the coordinates is out of the bounds!");
            }
            else
            {
                return this._tileCollection[yCoordinate, xCoordinate];
            }
        }

        public void SetGridCoordinate(int xCoordinate, int yCoordinate, bool state)
        {
            if (IsOutOfBounds(xCoordinate, yCoordinate))
            {
                throw new InvalidOperationException("One of the coordinates is out of the bounds!"); //TODO: konkrétan meghatározni, hogy melyik koordináta nem jó (x vagy y)
            }
            else
            {
                this._tileCollection[yCoordinate, xCoordinate] = state;
            }
        }

        public void Clear()
        {
            this._tileCollection = new bool[this.YLength, this.XLength]; ;
        }

        /// <summary>
        /// Az adott koordináta a hátáron kívülre mutat
        /// </summary>
        /// <param name="xCoordinate">Oszlopszám</param>
        /// <param name="yCoordinate">Sorszám</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private bool IsOutOfBounds(int xCoordinate, int yCoordinate)
        {
            if (xCoordinate < 0 || xCoordinate >= this.XLength)
            {
                return true;
            }
            else if (yCoordinate < 0 || yCoordinate >= this.YLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllTrue()
        {
            return this._tileCollection.Cast<bool>().All(x => x);
        }
    }
}
