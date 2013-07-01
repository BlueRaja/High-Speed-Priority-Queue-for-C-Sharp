using System.Collections.Generic;

namespace Priority_Queue
{
    /// <summary>
    /// The IPriorityQueue interface.  This is mainly here for purists, and in case I decide to add more implementations later.
    /// For speed purposes, it is actually recommended that you *don't* access the priority queue through this interface, since the JIT can
    /// (theoretically?) optimize method calls from concrete-types slightly better.
    /// </summary>
    public interface IPriorityQueue<T> : IEnumerable<T>
        where T : IPriorityQueueNode
    {
        void Remove(T node);
        void UpdatedPriority(T node);
        void Enqueue(T node);
        void Enqueue(IEnumerable<T> nodes);
        T Dequeue();
        T First { get; }
        int Count { get; }
        int MaxSize { get; }
        void Clear();
        bool Contains(T node);
    }
}
