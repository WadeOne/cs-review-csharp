using System;
using System.Collections.Generic;
using Xunit;


namespace CSReview.DataStructures
{
    public class BinarySearchTree<TKey,TValue>
    {
        public BSTreeNode<TKey,TValue> Root { get; private set; } = null;
        private Comparer<TKey> _comparer;

        public BinarySearchTree(Comparer<TKey> comparer)
        {
            _comparer = comparer;
        }

        public BinarySearchTree() : this(Comparer<TKey>.Default)
        { }

        public BinarySearchTree(TKey key, Comparer<TKey> comparer)
        {
            _comparer = comparer;
            Root = new BSTreeNode<TKey,TValue> {Key = key};
        }

        public BinarySearchTree(TKey key) : this(key, Comparer<TKey>.Default)
        { }

        public BSTreeNode<TKey,TValue> Insert(TKey key, TValue value = default)
        {
            var newNode = new BSTreeNode<TKey,TValue> {Key = key, Value = value};
            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                var trailing = Root;
                var current = Root;
                while (current != null)
                {
                    trailing = current;
                    if (_comparer.Compare(key, current.Key) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (_comparer.Compare(key, trailing.Key) < 0)
                {
                    trailing.Left = newNode;
                }
                else
                {
                    trailing.Right = newNode;
                }

                newNode.Parent = trailing;
            }

            return newNode;
        }

        public void InorderTraversal(Action<TKey> processNode)
        {
            InorderTraversal(Root, processNode);
        }

        private void InorderTraversal(BSTreeNode<TKey,TValue> node, Action<TKey> processNode)
        {
            if (node == null) return;
            
            InorderTraversal(node.Left, processNode);
            processNode(node.Key);
            InorderTraversal(node.Right, processNode);
        }

        public BSTreeNode<TKey, TValue> Min()
        {
            return Min(Root);
        }

        public BSTreeNode<TKey,TValue> Min(BSTreeNode<TKey,TValue> root)
        {
            if (root == null) return null;

            var node = root;
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        public BSTreeNode<TKey,TValue> SearchByKey(TKey key)
        {
            if (Root == null) return null;
            var node = Root;
            while (node != null && !key.Equals(node.Key))
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }

            return node;
        }

        public BSTreeNode<TKey,TValue> Successor(TKey key)
        {
            var node = SearchByKey(key);
            if (node == null) return null;

            if (node.Right != null)
            {
                return Min(node.Right);
            }

            var parent = node.Parent;
            while (parent != null && parent.Right == node)
            {
                node = parent;
                parent = node.Parent;
            }

            return parent;
        }

        public BSTreeNode<TKey, TValue> RemoveByKey(TKey key)
        {
            var node = SearchByKey(key);
            if (node.Left == null)
            {
                Transplant(node, node.Right);
                return node;
            }

            if (node.Right == null)
            {
                Transplant(node, node.Left);
                return node;
            }

            var successor = Min(node.Right);
            if (successor.Parent != node)
            {
                Transplant(successor, successor.Right);
                successor.Right = node.Right;
                successor.Right.Parent = successor;
            }
            Transplant(node, successor);
            successor.Left = node.Left;
            successor.Left.Parent = successor;
            return node;

        }

        private void Transplant(BSTreeNode<TKey, TValue> target, BSTreeNode<TKey, TValue> toPlant)
        {
            //Root
            if (target.Parent == null)
            {
                Root = toPlant;
                return;
            }

            if (target.Parent.Left == target)
            {
                target.Parent.Left = toPlant;
            }
            else
            {
                target.Parent.Right = toPlant;
            }

            if (toPlant != null)
                toPlant.Parent = target.Parent;
        }
    }
    
    public class Tests
    {
        [Fact]
        public void Insert()
        {
            var tree = CreateTree();
            var lst = new List<int>();
            tree.InorderTraversal(x => lst.Add(x));
            Assert.Equal(new int[]{1,5,9,10,13,15,20}, lst.ToArray());
        }

        [Fact]
        public void Min()
        {
            var tree = CreateTree();
            Assert.Equal(1, tree.Min().Key);
        }

        [Fact]
        public void SearchByKey()
        {
            var tree = CreateTree();
            Assert.Equal(42, tree.SearchByKey(13).Value);
            
            Assert.Null(tree.SearchByKey(100));
        }

        [Fact]
        public void Successor()
        {
            var tree = CreateTree();
            Assert.Equal(10, tree.Successor(9).Key);
            Assert.Equal(5, tree.Successor(1).Key);
            Assert.Equal(13, tree.Successor(10).Key);
            Assert.Null(tree.Successor(20));
        }

        [Fact]
        public void RemoveByKey()
        {
            var tree = CreateTree();
            tree.RemoveByKey(5);
            Assert.Null(tree.SearchByKey(5));
            tree.RemoveByKey(10);
            Assert.Null(tree.SearchByKey(10));
            
            var lst = new List<int>();
            tree.InorderTraversal(x => lst.Add(x));
            Assert.Equal(new int[]{1,9,13,15,20}, lst.ToArray());
        }

        private static BinarySearchTree<int,int> CreateTree()
        {
            var tree = new BinarySearchTree<int,int>(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(13, 42);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(1);
            return tree;
        }
    }

    public class BSTreeNode<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public BSTreeNode<TKey,TValue> Parent { get; set; }
        public BSTreeNode<TKey,TValue> Left { get; set; }
        public BSTreeNode<TKey,TValue> Right { get; set; }

        public override string ToString() => $"{{Key: {Key}, Val: {Value}}}";
    }
}