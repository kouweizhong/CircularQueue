namespace Circular_Queue
{
    using System;

    public class CircularQueue<T>
    {
        private const int InitialCapacity = 16;

        private T[] storage;

        private int capacity;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = InitialCapacity)
        {
            this.Capacity = capacity;
            this.storage = new T[this.Capacity];
        }

        public int Count { get; private set; }

        private int Capacity
        {
            get
            {
                return this.capacity;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Queue capacity should be a positive integer");
                }

                this.capacity = value;
            }
        }

        public void Enqueue(T element)
        {
            if (this.GrowNeeded())
            {
                this.Grow();
            }

            this.storage[this.endIndex] = element;
            this.endIndex = (this.endIndex + 1) % this.Capacity;
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var element = this.storage[this.startIndex];
            this.storage[this.startIndex] = default(T);
            this.startIndex = (this.startIndex + 1) % this.Capacity;
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            var resultArray = new T[this.Count];
            this.ResizeArrayStorage(resultArray);

            return resultArray;
        }

        private bool GrowNeeded()
        {
            var result = this.Count >= this.Capacity;

            return result;
        }

        private void Grow()
        {
            var newStorage = new T[this.Capacity * 2];
            this.ResizeArrayStorage(newStorage);
            this.startIndex = 0;
            this.endIndex = this.Capacity;
            this.Capacity *= 2;

            this.storage = newStorage;
        }

        private void ResizeArrayStorage(T[] array)
        {
            for (int i = 0; i < this.Count; i++)
            {
                var currentIndex = (this.startIndex + i) % this.Capacity;
                array[i] = this.storage[currentIndex];
            }
        }
    }


    public static class Example
    {
        public static void Main()
        {
            var queue = new CircularQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);

            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            var first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            queue.Enqueue(-7);
            queue.Enqueue(-8);
            queue.Enqueue(-9);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            queue.Enqueue(-10);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");
        }
    }
}