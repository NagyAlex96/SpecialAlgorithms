using System.Runtime.InteropServices;

namespace SpecialAlgorithms.FibonacciHeap
{
    public unsafe struct FibonacciHeap<T, TKey> : IDisposable
        where T : unmanaged
        where TKey : IComparable<TKey>
    {
        private FibonacciHeapNode* _minNode;
        private int _nodeCount;

        public FibonacciHeap()
        {
            this._minNode = null;
            this._nodeCount = 0;
        }

        /// <summary>
        /// <c>True</c>: if the heap is not empty
        /// </summary>
        public readonly bool IsEmpty => this._nodeCount == 0;
        /// <summary>
        /// Size of the heap
        /// </summary>
        public readonly int Size => this._nodeCount;
        /// <summary>
        /// Content of the min node
        /// </summary>
        public readonly T MinNodeContent
        {
            get
            {
                if (!IsEmpty)
                {
                    return this._minNode->Data;
                }
                throw new InvalidOperationException("MinNode doesn't exists. The heap is empty!");
            }
        }
        /// <summary>
        /// Key of the min node
        /// </summary>
        public readonly TKey MinNodeKey
        {
            get
            {
                if (!IsEmpty)
                {
                    return this._minNode->Key;
                }
                throw new InvalidOperationException("MinNode doesn't exists. The heap is empty!");
            }
        }
        /// <summary>
        /// Content & Key of the min node
        /// </summary>
        public readonly (T, TKey) MinNodeInfo
        {
            get
            {
                if (!IsEmpty)
                {
                    return (this._minNode->Data, this._minNode->Key);
                }
                throw new InvalidOperationException("The heap is empty!");
            }
        }

        /// <summary>
        /// Insert content into the heap with the given key
        /// </summary>
        /// <param name="content">Inserted content</param>
        /// <param name="key">Given key. This specify the order of the heap</param>
        public void Insert(T content, TKey key)
        {
            FibonacciHeapNode* newNode = (FibonacciHeapNode*)Marshal.AllocHGlobal(sizeof(FibonacciHeapNode));
            *newNode = new FibonacciHeapNode(content, key);

            //nincs még elem a listában
            if (this._minNode == null)
            {
                newNode->Left = newNode;
                newNode->Right = newNode;
                this._minNode = newNode;
            }
            else
            {
                InsertToRootList(ref newNode);

                //amennyiben az új csomópont kulcsa kisebb, mint a jelenlegi min.kulcs
                if (newNode->Key.CompareTo(this._minNode->Key) < 0)
                {
                    this._minNode = newNode; //az új kulcs lesz a halom új minimuma
                }
            }
            this._nodeCount++;
        }

        public static FibonacciHeap<T, TKey> Union(FibonacciHeap<T, TKey> heapA, FibonacciHeap<T, TKey> heapB)
        {
            FibonacciHeap<T, TKey> newHeap = new FibonacciHeap<T, TKey>();
            newHeap._minNode = heapA._minNode;

            if (heapA._minNode != null && heapB._minNode != null)
            {
                heapA._minNode->Right = heapB._minNode->Right;
                heapB._minNode->Left = heapB._minNode->Left;

                heapA._minNode->Right = heapB._minNode;
                heapB._minNode->Left = heapA._minNode;

                heapA._minNode->Right->Left = heapB._minNode->Left;
                heapB._minNode->Left->Right = heapA._minNode->Right;

                if (heapB._minNode->Key.CompareTo(heapA._minNode->Key) < 1)
                {
                    newHeap._minNode = heapB._minNode;
                }
            }
            else if (heapA._minNode == null)
            {
                newHeap._minNode = heapB._minNode;
            }

            newHeap._nodeCount = heapA._nodeCount + heapB._nodeCount;

            return newHeap;
        }

        public T ExtractMin()
        {
            // Step 1: Get the minimum node
            FibonacciHeapNode* tempMinNode = this._minNode;

            if (tempMinNode != null)
            {
                // Step 2: Move all children of minNode to the root list
                if (tempMinNode->Child != null)
                {
                    // Traverse through the children and add them to the root list
                    FibonacciHeapNode* child = tempMinNode->Child;
                    do
                    {
                        FibonacciHeapNode* nextChild = child->Right;

                        // Remove child from its current position
                        child->Left->Right = child->Right;
                        child->Right->Left = child->Left;

                        // Add child to the root list
                        child->Left = this._minNode->Left;
                        child->Right = this._minNode;
                        this._minNode->Left->Right = child;
                        this._minNode->Left = child;

                        // Set parent to null
                        child->Parent = null;

                        child = nextChild;
                    } while (child != tempMinNode->Child);
                }

                // Step 3: Remove minNode from the root list
                if (tempMinNode->Right == tempMinNode) // only one node in the list
                {
                    this._minNode = null;
                }
                else
                {
                    tempMinNode->Left->Right = tempMinNode->Right;
                    tempMinNode->Right->Left = tempMinNode->Left;
                    this._minNode = tempMinNode->Right;
                }

                // Step 4: Perform consolidation
                Consolidate();

                // Decrease node count
                this._nodeCount--;
            }

            // Return the removed minimum node
            return tempMinNode->Data;
        }

        private void Consolidate()
        {
            int size = (int)Math.Log2(this._nodeCount) + 1;
            FibonacciHeapNode** A = (FibonacciHeapNode**)(Marshal.AllocHGlobal(size * sizeof(FibonacciHeapNode*))); //TODO: helyfoglalás (ALLOC)
            for (int i = 0; i < size; i++)
            {
                A[i] = null;
            }


            FibonacciHeapNode* current = this._minNode;
            int rootCount = 0;

            if (current != null)
            {
                do
                {
                    rootCount++;
                    current = current->Right;
                } while (current != this._minNode);
            }


            while (rootCount > 0)
            {
                FibonacciHeapNode* x = current;
                int degree = x->Degree;
                FibonacciHeapNode* next = current->Right;

                while (A[degree] != null) //TODO
                {
                    FibonacciHeapNode* y = A[degree];

                    if (x->Key.CompareTo(y->Key) > 0)
                    {
                        FibonacciHeapNode* temp = x;
                        x = y;
                        y = temp;
                    }

                    // Link y to x
                    Link(ref y, ref x);
                    A[degree] = null;
                    degree++;
                }

                A[degree] = x;
                current = next;
                rootCount--;
            }

            Marshal.FreeHGlobal((IntPtr)A);
        }
        private void CascadingCut(ref FibonacciHeapNode* node)
        {
            var z = node->Parent;
            if (z != null)
            {
                if (!node->Mark)
                {
                    node->Mark = true;
                }
                else
                {
                    Cut(ref node, ref z);
                    CascadingCut(ref z);
                }
            }
        }
        private void Link(ref FibonacciHeapNode* nodeA, ref FibonacciHeapNode* nodeB)
        {
            nodeA->Left->Right = nodeA->Right;
            nodeA->Right->Left = nodeA->Left;

            nodeA->Parent = nodeB;

            if (nodeB->Child == null)
            {
                nodeB->Child = nodeA;
                nodeA->Right = nodeA;
                nodeA->Left = nodeA;
            }
            else
            {
                nodeA->Left = nodeB->Child;
                nodeA->Right = nodeB->Child->Right;
                nodeB->Child->Right->Left = nodeA;
                nodeB->Child->Right = nodeA;
            }

            nodeB->Degree++;
            nodeA->Mark = false;
        }
        private void Cut(ref FibonacciHeapNode* nodeA, ref FibonacciHeapNode* nodeB)
        {
            if (nodeA->Right == nodeA)
            {
                nodeB->Child = null;
            }
            else
            {
                nodeA->Left->Right = nodeA->Right;
                nodeA->Right->Left = nodeA->Left;
                if (nodeB->Child == nodeA)
                {
                    nodeB->Child = nodeA->Right;
                }
                nodeB->Degree -= 1;
            }
            InsertToRootList(ref nodeA);
            nodeA->Parent = null;
            nodeA->Mark = false;
        }

        /// <summary>
        /// Insert node into the heap's root list
        /// </summary>
        /// <param name="node">Node to insert</param>
        private void InsertToRootList(ref FibonacciHeapNode* node)
        {
            node->Left = this._minNode;
            node->Right = this._minNode->Right;
            this._minNode->Right->Left = node;
            this._minNode->Right = node;
        }
        public void Dispose()
        {
            Dispose(this._minNode);

            this._minNode = null;
            this._nodeCount = 0;
            GC.SuppressFinalize(this);
        }
        private void Dispose(FibonacciHeapNode* node)
        {
            if (node != null)
            {
                FibonacciHeapNode* temp = node;
                do
                {
                    FibonacciHeapNode* temp2 = temp;
                    temp = temp->Right;
                    Dispose(temp2->Child);
                    Marshal.FreeHGlobal((IntPtr)temp2);
                } while (temp != node);
            }
        }

        private struct FibonacciHeapNode
        {
            /// <summary>
            /// Stored data
            /// </summary>
            public T Data;
            /// <summary>
            /// Key of the node
            /// </summary>
            public TKey Key;
            /// <summary>
            /// Has any child removed since the last visit?
            /// </summary>
            public bool Mark;
            /// <summary>
            /// Number of children of the node
            /// </summary>
            public int Degree;
            /// <summary>
            /// Left neighbour of the node
            /// </summary>
            public FibonacciHeapNode* Left;
            /// <summary>
            /// Right neighbour of the node
            /// </summary>
            public FibonacciHeapNode* Right;
            /// <summary>
            /// Parent of the node (if it has)
            /// </summary>
            public FibonacciHeapNode* Parent;
            /// <summary>
            /// One of the children of the node (if it has)
            /// </summary>
            public FibonacciHeapNode* Child;

            public FibonacciHeapNode(T data, TKey key)
            {
                this.Data = data;
                this.Key = key;

                this.Degree = 0;
                this.Mark = false;
                this.Left = null;
                this.Right = null;
                this.Child = null;
                this.Parent = null;
            }
            public FibonacciHeapNode()
            {
                this.Data = default(T);
                this.Key = default(TKey);

                this.Degree = 0;
                this.Mark = false;
                this.Left = null;
                this.Right = null;
                this.Child = null;
                this.Parent = null;
            }

        }
    }
}
