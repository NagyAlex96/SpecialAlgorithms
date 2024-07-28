using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAlgorithms.LinkedList
{
    public unsafe struct LinkedListOrdered<TContent, TKey> : IDisposable
        where TContent : unmanaged
        where TKey : unmanaged, IComparable<TKey>
    {
        private int _count = 0;
        public int Count => this._count;

        private LinkedListItem* _head;
        private LinkedListItem* _tail;

        public LinkedListOrdered()
        {
            this._head = null;
            this._tail = null;
        }

        public void BeszurasRovidebbValtozat(TContent ertek, TKey kulcs)
        {
            LinkedListItem* newContent = (LinkedListItem*)Marshal.AllocHGlobal(sizeof(LinkedListItem));
            *newContent = new LinkedListItem();

            newContent->Key = kulcs;
            newContent->Content = ertek;

            LinkedListItem* p = this._head;
            LinkedListItem* e = null;

            while (p != null && p->Key.CompareTo(kulcs) < 0)
            {
                e = p;
                p = p->NextItem;
            }

            if (e == null)
            {
                newContent->NextItem = this._head;
                this._head = newContent;
            }
            else
            {
                newContent->NextItem = p;
                e->NextItem = newContent;
            }
            this._count++;
        }


        public void Dispose()
        {
            LinkedListItem* current = this._head;
            for (int i = 0; i < this._count; i++)
            {
                LinkedListItem* temp = current;
                current = current->NextItem;
                Dispose(temp);
            }

            this._head = null;
            this._tail = null;
            this._count = 0;

            GC.SuppressFinalize(this);
        }
        private void Dispose(LinkedListItem* item)
        {
            item->NextItem = null;
            item->PreviousItem = null;
            Marshal.FreeHGlobal((IntPtr)item);
        }


        private struct LinkedListItem
        {
            public TContent Content;

            public TKey Key;

            public LinkedListItem* NextItem;

            public LinkedListItem* PreviousItem;

            public LinkedListItem()
            {
                NextItem = null;
                PreviousItem = null;
            }
        }
    }
}
