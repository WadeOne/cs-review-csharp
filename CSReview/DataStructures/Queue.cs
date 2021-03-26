using System;
using Xunit;

namespace CSReview.DataStructures
{
    public class Queue<T>
    {
        private T[] _queue;
        private readonly int _size;

        private int _head;
        private int _tail;
        private bool _isFull;

        public Queue(int size)
        {
            _size = size;
            _head = 0;
            _queue = new T[_size];
        }

        public void Enqueue(T elem)
        {
            if (_isFull) throw new OverflowException();

            _queue[_tail] = elem;

            if (_tail < _size - 1)
            {
                _tail++;
            }
            else
            {
                _tail = 0;
                if (_head == 0) _isFull = true;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException();

            var toReturn = _queue[_head];
            if (_head < _size - 1)
            {
                _head++;
            }
            else
            {
                _head = 0;
            }

            _isFull = false;

            return toReturn;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException();

            return _queue[_head];
        }

        public bool IsEmpty()
        {
            return _head == _tail;
        }

        public void Clear()
        {
            _head = 0;
            _tail = 0;
            _isFull = false;
        }
        
        
    }

    public class Queue
    {
        [Fact]
        public void BordersTest()
        {
            var queue = new Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.Throws<OverflowException>(() => queue.Enqueue(4));
            
            queue.Clear();
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }
        
        [Fact]
        public void OperationsAreConsistent()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var queue = new Queue<int>(10);
            foreach (var el in arr)
            {
                queue.Enqueue(el);
            }

            foreach (var el in arr)
            {
                var saved = queue.Dequeue();
                Assert.Equal(el, saved);
            }
            
            Assert.True(queue.IsEmpty());
        }
    }
}