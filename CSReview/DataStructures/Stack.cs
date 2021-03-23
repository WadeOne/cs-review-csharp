using System;
using Xunit;

namespace CSReview.DataStructures
{
    public class Stack<T>
    {
        private readonly int _size = -1;
        private T[] _stack;
        private const int _defaultSize = 256;
        private int _stackTop = 0;

        public int Size => _stack.Length;
        public int Count => _stackTop;
        
        public Stack(int size)
        {
            _size = size;
            _stack = new T[_size];
        }

        public Stack()
        {
            _stack = new T[_defaultSize];
        }

        public void Push(T elem)
        {
            if (_stackTop == _size) throw new StackOverflowException();

            var stackLength = _stack.Length;
            if (_size == -1 && _stackTop == stackLength >> 1)
            {
                _stack = Resize(_stack, stackLength * 2);
            }

            _stack[_stackTop] = elem;
            _stackTop++;
        }

        public T Peek()
        {
            if (_stackTop == 0) throw new InvalidOperationException();

            return _stack[_stackTop - 1];
        }

        public T Pop()
        {
            if (_stackTop == 0) throw new InvalidOperationException();

            var toReturn = _stack[_stackTop - 1];
            _stackTop--;

            var stackLength = _stack.Length;
            if (_size == -1 && _stackTop == stackLength >> 2)
            {
                _stack = Resize(_stack, (stackLength >> 1) + (stackLength >> 2));
            }

            return toReturn;
        }

        private T[] Resize(T[] stack, int newSize)
        {
            var newStack = new T[newSize];
            for (int i = 0; i < _stackTop; i++)
            {
                newStack[i] = stack[i];
            }

            return newStack;
        }
    }

    public class Tests
    {
        [Fact]
        public void BordersTest()
        {
            var stack = new Stack<int>(1);

            Assert.Throws<InvalidOperationException>(() => stack.Peek());
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
            
            stack.Push(42);
            Assert.Throws<StackOverflowException>(() => stack.Push(69));
        }

        [Fact]
        public void OperationsAreConsistent_FixedSizeStack()
        {
            var elementsNum = 100;
            var stack = new Stack<int>(elementsNum + 1);
            for (int i = 0; i < elementsNum; i++)
            {
                stack.Push(i);
            }

            for (int i = elementsNum; i <= 0; i--)
            {
                Assert.Equal(i, stack.Pop());
            }
        }

        [Fact]
        public void OperationsAreConsistent_DynamicallySizeStack()
        {
            var elementsNum = 1000;
            var stack = new Stack<int>();
            for (int i = 0; i < elementsNum; i++)
            {
                stack.Push(i);
            }

            for (int i = elementsNum; i <= 0; i--)
            {
                Assert.Equal(i, stack.Pop());
            }
        }

        [Fact]
        public void DynamicallySizedStack_ResizesProperly()
        {
            var stack = new Stack<int>();
            var defaultSize = stack.Size;

            for (int i = 0; i <= defaultSize; i++)
            {
                stack.Push(i);
            }
            Assert.Equal(defaultSize << 2, stack.Size);

            stack.Pop();
            Assert.Equal(defaultSize * 3, stack.Size);
        }
    }
}