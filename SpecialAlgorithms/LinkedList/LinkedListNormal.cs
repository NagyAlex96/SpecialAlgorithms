using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAlgorithms.LinkedList
{
    public unsafe struct LinkedListNormal<TContent> : IDisposable
        where TContent : unmanaged, IComparable<TContent>
    {
        private int _count = 0;
        public int Count => this._count;

        private LinkedListItem* _head;
        private LinkedListItem* _tail;


        public LinkedListNormal()
        {
            this._head = null;
            this._tail = null;
        }

        public void InsertBeforeTheFirstElement(TContent content)
        {
            LinkedListItem* newContent = (LinkedListItem*)Marshal.AllocHGlobal(sizeof(LinkedListItem));
            *newContent = new LinkedListItem();
            newContent->Content = content;
            newContent->NextItem = this._head;

            if (this._tail == null)
            {
                this._tail = newContent;
            }

            if (this._head != null)
            {
                newContent->PreviousItem = this._tail;
                this._tail->NextItem = newContent;
                this._head->PreviousItem = newContent;
            }

            this._head = newContent;
            this._count++;
        }

        public void InsertAfterTheLastElement(TContent content)
        {
            LinkedListItem* newContent = (LinkedListItem*)Marshal.AllocHGlobal(sizeof(LinkedListItem));
            *newContent = new LinkedListItem();

            newContent->Content = content;
            newContent->PreviousItem = this._tail;

            if (this._head == null)
            {
                this._head = newContent;
            }

            if (this._tail != null)
            {
                newContent->NextItem = this._head;
                this._head->PreviousItem = newContent;
                this._tail->NextItem = newContent;
            }

            this._tail = newContent;
            this._count++;
        }

        public void InsertN_Position(TContent content, int index)
        {
            if (this._head == null || index <= 0)
            {
                InsertBeforeTheFirstElement(content);
            }
            else if (index >= Count)
            {
                InsertAfterTheLastElement(content);
            }
            else
            {
                LinkedListItem* newContent = (LinkedListItem*)Marshal.AllocHGlobal(sizeof(LinkedListItem));
                *newContent = new LinkedListItem();
                newContent->Content = content;

                int forward = 0 + index;
                int backward = this.Count - 1 - index;

                if (backward >= forward)
                {
                    Forward(ref newContent, index);
                }
                else
                {
                    Backward(ref newContent, index);
                }
                this._count++;
            }
        }
        private void Forward(ref LinkedListItem* item, int NPosition)
        {
            var temp = this._head;
            int i = 1;
            while (i < Count && i != NPosition)
            {
                temp = temp->NextItem;
            }
            item->NextItem = temp->NextItem;
            item->PreviousItem = temp;

            temp->NextItem->PreviousItem = item;
            temp->NextItem = item;
        }
        private void Backward(ref LinkedListItem* item, int NPosition)
        {
            var temp = this._tail;
            int i = this.Count - 2;
            while (i > 0 && i != NPosition)
            {
                temp = temp->PreviousItem;
            }
            item->NextItem = temp;
            item->PreviousItem = temp->PreviousItem;

            temp->PreviousItem->NextItem = item;
            temp->PreviousItem = item;
        }

        public delegate void TraversalDelegate(TContent content);
        public void TraversalForward(TraversalDelegate traversalDelegate)
        {
            LinkedListItem* p = this._head;
            for (int i = 0; i < Count; i++)
            {
                traversalDelegate(p->Content);
                p = p->NextItem;
            }
        }
        public void TraversalBackward(TraversalDelegate traversalDelegate)
        {
            LinkedListItem* p = this._tail;

            for (int i = Count; i > 0; i--)
            {
                traversalDelegate(p->Content);
                p = p->PreviousItem;
            }
        }

        public (bool, TContent?) LinearSearch(Func<TContent, bool> condition)
        {
            LinkedListItem* p = this._head;
            LinkedListItem* t = this._tail;

            int i = 0;
            int j = Count;
            while (i < Count / 2 && j > Count / 2 && (!condition(p->Content) && !condition(t->Content)))
            {
                p = p->NextItem;
                t = t->PreviousItem;
                i++;
                j--;
            }

            bool van = (i < Count / 2 && j > Count / 2);

            if (van)
            {
                return (true, condition(p->Content) ? p->Content : t->Content);
            }
            else
            {
                return (false, null);
            }
        }
        public (bool, TContent?) LinearSearch(TContent seachedItem)
        {
            LinkedListItem* p = this._head;
            LinkedListItem* t = this._tail;

            int i = 0;
            int j = Count;
            while (i < Count / 2 && j > Count / 2 && (p->Content.CompareTo(seachedItem) != 0 && t->Content.CompareTo(seachedItem) != 0))
            {
                p = p->NextItem;
                t = t->PreviousItem;
                i++;
                j--;
            }

            bool van = (i < Count / 2 && j > Count / 2);

            if (van)
            {
                return (true, p->Content.CompareTo(seachedItem) == 0 ? p->Content : t->Content);
            }
            else
            {
                return (false, null);
            }
        }

        public void DeleteItem(TContent content)
        {
            LinkedListItem* p = this._head;
            LinkedListItem* e = null;
            int i = 0;

            while (i < this.Count && p->Content.CompareTo(content) != 0)
            {
                e = p;
                p = p->NextItem;
                i++;
            }

            if (p != null)
            {
                if (e == null) //delete the first element
                {
                    this._head = p->NextItem;
                    this._tail->NextItem = p->NextItem;
                    this._head->PreviousItem = this._tail;
                }
                else //between first and the last element
                {
                    e->NextItem = p->NextItem;
                    e->NextItem->PreviousItem = p->PreviousItem;
                }

                if (i == this.Count) //delete the last element
                {
                    this._tail = e;
                }

                Dispose(p);
            }
            else
            {
                throw new InvalidOperationException("There is no item with the give content!");
            }
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
