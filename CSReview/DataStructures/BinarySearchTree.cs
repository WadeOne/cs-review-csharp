using System;
using System.Collections;
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

        public BSTreeNode<TKey,TValue> Min()
        {
            if (Root == null) return null;

            var node = Root;
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

        // public BSTreeNode<TKey,TValue> Successor(TKey key)
        // {
        // }
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
    }
}