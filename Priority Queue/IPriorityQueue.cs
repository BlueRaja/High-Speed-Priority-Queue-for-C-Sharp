using System.Collections.Generic;

namespace Priority_Queue
{
    public interface IPriorityQueue<T> : IEnumerable<T>
        where T : IPriorityQueueNode
    {
        void Remove(T node);
        void UpdatedPriority(T node);
        void Enqueue(T node);
        T Dequeue();
        T First { get; }
        int Count { get; }
        void Clear();
        bool Contains(T node);
    }
}
