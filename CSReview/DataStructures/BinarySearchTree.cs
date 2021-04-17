using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace CSReview.DataStructures
{
    public class BinarySearchTree<TValue>
    {
        public BSTreeNode<TValue> Root { get; private set; } = null;
        private Comparer<TValue> _comparer;

        public BinarySearchTree(Comparer<TValue> comparer)
        {
            _comparer = comparer;
        }

        public BinarySearchTree() : this(Comparer<TValue>.Default)
        { }

        public BinarySearchTree(TValue value, Comparer<TValue> comparer)
        {
            _comparer = comparer;
            Root = new BSTreeNode<TValue> {Value = value};
        }

        public BinarySearchTree(TValue value) : this(value, Comparer<TValue>.Default)
        { }

        public BSTreeNode<TValue> Insert(TValue value)
        {
            var newNode = new BSTreeNode<TValue> {Value = value};
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
                    if (_comparer.Compare(value, current.Value) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (_comparer.Compare(value, trailing.Value) < 0)
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

        public void InorderTraversal(Action<TValue> processNode)
        {
            InorderTraversal(Root, processNode);
        }

        private void InorderTraversal(BSTreeNode<TValue> node, Action<TValue> processNode)
        {
            if (node == null) return;
            
            InorderTraversal(node.Left, processNode);
            processNode(node.Value);
            InorderTraversal(node.Right, processNode);
        }


        
    }
    
    public class Tests
    {
        [Fact]
        public void InsertTest()
        {
            var tree = new BinarySearchTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(13);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(1);

            var lst = new List<int>();
            tree.InorderTraversal(x => lst.Add(x));
            
            Assert.Equal(new int[]{1,5,9,10,13,15,20}, lst.ToArray());
        }
    }

    public class BSTreeNode<TValue>
    {
        public TValue Value { get; set; }
        public BSTreeNode<TValue> Parent { get; set; }
        public BSTreeNode<TValue> Left { get; set; }
        public BSTreeNode<TValue> Right { get; set; }
    }
}