using Xunit;
using static System.Math;

namespace CSReview.DataStructures
{
    public class AVLTree
    {
        public class TreeNode
        {
            public int Val { get; set; }
            public int Height { get; set; } = 1;
            
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(int val)
            {
                Val = val;
            }
        }
        
        public TreeNode Root { get; private set; }

        public AVLTree(int val)
        {
            Root = new TreeNode(val);
        }

        private TreeNode RightRotate(TreeNode node)
        {
            var x = node.Left;
            var y = x.Right;

            x.Right = node;
            node.Left = y;
            
            node.Height = Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            x.Height = Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private TreeNode LeftRotate(TreeNode node)
        {
            var x = node.Right;
            var y = x.Left;

            x.Left = node;
            node.Right = y;

            node.Height = Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            x.Height = Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        public TreeNode Insert(int val)
        {
            return Insert(Root, val);
        }

        private TreeNode Insert(TreeNode node, int val)
        {
            if (node == null)
                return new TreeNode(val);

            if (val < node.Val)
            {
                node.Left = Insert(node.Left, val);
            }
            else
            {
                node.Right = Insert(node.Right, val);
            }

            node.Height = Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            var balance = GetHeight(node.Left) - GetHeight(node.Right);

            if (balance > 1 && val < node.Left.Val)
            {
                return RightRotate(node);
            }

            if (balance > 1 && val > node.Left.Val)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            if (balance < -1 && val > node.Right.Val)
            {
                return LeftRotate(node);
            }

            if (balance < -1 && val < node.Right.Val)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public bool IsBalanced()
        {
            return IsBalanced(Root);
        }

        private bool IsBalanced(TreeNode root)
        {
            if (root == null) return true;

            if (Abs(GetHeight(root.Left) - GetHeight(root.Right)) > 1)
                return false;

            return IsBalanced(root.Left) && IsBalanced(root.Right);
        }

        private int GetHeight(TreeNode node) => node?.Height ?? 0;

        public class Tests
        {
            [Fact]
            public void IsBalanced()
            {
                var tree = new AVLTree(10);
                tree.Insert(20);
                tree.Insert(30);
                tree.Insert(40);
                tree.Insert(50);
                tree.Insert(25);
                Assert.True(tree.IsBalanced());

                tree = new AVLTree(20);
                tree.Insert(19);
                tree.Insert(15);
                tree.Insert(13);
                tree.Insert(8);
                tree.Insert(2);
                tree.Insert(1);
                Assert.True(tree.IsBalanced());
            }
        }
    }
    
}