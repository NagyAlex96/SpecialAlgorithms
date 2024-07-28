using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAlgorithms.BinaryTree
{
    public unsafe struct BinaryTree<TContent, TKey> : IDisposable
                                                    where TContent : unmanaged, IComparable<TContent>
                                                    where TKey : IComparable<TKey>
    {
        private TreeItem* _root;
        public BinaryTree()
        {
            this._root = null;
        }

        //TODO: bejárások megírása a következő delegatek-re is. Csak TContent és csak TKey


        public delegate void TraversalDelegate(TContent content, TKey key);

        public void PreOrderTraversal(TraversalDelegate traversalDelegate)
        {
            PreOrderTraversalAssistant(this._root, traversalDelegate);
        }
        private void PreOrderTraversalAssistant(TreeItem* p, TraversalDelegate traversal)
        {
            if (p != null)
            {
                traversal(p->Content, p->Key);
                PreOrderTraversalAssistant(p->LeftChild, traversal);
                PreOrderTraversalAssistant(p->RightChild, traversal);
            }
        }

        public void InOrderTraversal(TraversalDelegate traversalDelegate)
        {
            InOrderTraversalAssistant(this._root, traversalDelegate);
        }
        private void InOrderTraversalAssistant(TreeItem* p, TraversalDelegate traversal)
        {
            if (p != null)
            {
                InOrderTraversalAssistant(p->LeftChild, traversal);
                traversal(p->Content, p->Key);
                InOrderTraversalAssistant(p->RightChild, traversal);
            }
        }

        public void PostOrderTraversal(TraversalDelegate traversalDelegate)
        {
            PostOrderTraversalAssistant(this._root, traversalDelegate);
        }
        private void PostOrderTraversalAssistant(TreeItem* p, TraversalDelegate traversal)
        {
            if (p != null)
            {
                PostOrderTraversalAssistant(p->LeftChild, traversal);
                PostOrderTraversalAssistant(p->RightChild, traversal);
                traversal(p->Content, p->Key);
            }
        }


        public void Insert(TContent content, TKey key)
        {
            InsertAssistant(ref this._root, content, key);
        }
        private void InsertAssistant(ref TreeItem* parent, TContent content, TKey key)
        {
            if (parent == null)
            {
                parent = (TreeItem*)Marshal.AllocHGlobal(sizeof(TreeItem));
                *parent = new TreeItem(content, key);
            }
            else
            {
                if (parent->Key.CompareTo(key) > 0)
                {
                    InsertAssistant(ref parent->LeftChild, content, key);
                }
                else
                {
                    if (parent->Key.CompareTo(key) < 0)
                    {
                        InsertAssistant(ref parent->RightChild, content, key);
                    }
                    else
                    {
                        throw new InvalidOperationException("This key is already exists");
                    }
                }
            }

        }

        public TContent SearchElement(TKey key)
        {
            return SearchAssistant(ref this._root, key);
        }
        private TContent SearchAssistant(ref TreeItem* parent, TKey key)
        {
            if (parent != null)
            {
                if (parent->Key.CompareTo(key) > 0)
                {
                    return SearchAssistant(ref parent->LeftChild, key);
                }
                else
                {
                    if (parent->Key.CompareTo(key) < 0)
                    {
                        return SearchAssistant(ref parent->RightChild, key);
                    }
                    else
                    {
                        return parent->Content;
                    }
                }
            }
            else
            {
                throw new Exception("Element was not found with this key!");
            }
        }

        public void DeleteContent(TKey kulcs)
        {
            DeleteAssistant(ref this._root, kulcs);
        }
        private void DeleteAssistant(ref TreeItem* parent, TKey key)
        {
            if (parent != null)
            {
                if (parent->Key.CompareTo(key) > 0)
                {
                    DeleteAssistant(ref parent->LeftChild, key);
                }
                else
                {
                    if (parent->LeftChild == null)
                    {
                        var q = parent;
                        parent = parent->RightChild;
                        Dispose(q);
                    }
                    else
                    {
                        if (parent->RightChild == null)
                        {
                            var q = parent;
                            parent = parent->LeftChild;
                            Dispose(q);
                        }
                        else
                        {
                            DeleteBothChild(parent, ref parent->LeftChild);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("This key doesn't exists in this tree!");
            }
        }
        private void DeleteBothChild(TreeItem* e, ref TreeItem* r)
        {
            if (r->RightChild != null)
            {
                DeleteBothChild(e, ref r->RightChild);
            }
            else
            {
                e->Content = r->Content;
                e->Key = r->Key;
                var q = r;
                r = r->LeftChild;
                Dispose(q);
            }
        }

        public void Dispose()
        {
            Dispose(this._root);
            _root = null;
            GC.SuppressFinalize(this);
        }
        private void Dispose(TreeItem* node)
        {
            if (node != null)
            {
                Dispose(node->LeftChild);
                Dispose(node->RightChild);

                node->LeftChild = null;
                node->RightChild = null;
                Marshal.FreeHGlobal((IntPtr)node);
            }
        }

        private struct TreeItem
        {
            public TContent Content;

            public TKey Key;

            public TreeItem* LeftChild;

            public TreeItem* RightChild;

            public TreeItem(TContent content, TKey key)
            {
                this.Content = content;
                this.Key = key;
                this.LeftChild = null;
                this.RightChild = null;
            }

            public TreeItem()
            {

            }

        }
    }
}
