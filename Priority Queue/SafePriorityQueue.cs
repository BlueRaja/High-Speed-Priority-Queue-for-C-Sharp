using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priority_Queue
{
    //TODO: Make sure we're doing at least all the same checks as FastPriorityQueue does in DEBUG mode
    public sealed class SafePriorityQueue<T> : IPriorityQueue<T>
    {
        private class SafeNode : PriorityQueueNode
        {
            public T Data { get; private set; }

            public SafeNode(T data)
            {
                Data = data;
            }
        }

        private const int INITIAL_QUEUE_SIZE = 10;
        private readonly FastPriorityQueue<SafeNode> _queue;

        public SafePriorityQueue()
        {
            _queue = new FastPriorityQueue<SafeNode>(INITIAL_QUEUE_SIZE);
        }

        /// <summary>
        /// Returns the number of nodes in the queue.
        /// O(1)
        /// </summary>
        public int Count
        {
            get
            {
                lock(_queue)
                {
                    return _queue.Count;
                }
            }
        }


        /// <summary>
        /// Returns the head of the queue, without removing it (use Dequeue() for that).
        /// Throws an exception when the queue is empty.
        /// O(1)
        /// </summary>
        public T First
        {
            get
            {
                lock(_queue)
                {
                    if(_queue.Count <= 0)
                    {
                        throw new InvalidOperationException("Cannot call .First on an empty queue");
                    }

                    SafeNode first = _queue.First;
                    return (first != null ? first.Data : default(T));
                }
            }
        }

        /// <summary>
        /// Removes every node from the queue.
        /// O(n)
        /// </summary>
        public void Clear()
        {
            lock(_queue)
            {
                _queue.Clear();
            }
        }

        /// <summary>
        /// Returns whether the given item is in the queue.
        /// O(n)
        /// </summary>
        public bool Contains(T item)
        {
            lock(_queue)
            {
                var comparer = EqualityComparer<T>.Default;
                return _queue.Any(o => comparer.Equals(o.Data, item));
            }
        }

        /// <summary>
        /// Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
        /// If queue is empty, throws an exception
        /// O(log n)
        /// </summary>
        public T Dequeue()
        {
            lock(_queue)
            {
                if(_queue.Count <= 0)
                {
                    throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
                }

                SafeNode node =_queue.Dequeue();
                return node.Data;
            }
        }

        /// <summary>
        /// Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken by first-in-first-out.
        /// This queue automatically resizes itself, so there's no concern of the queue becoming 'full'.
        /// Duplicates are allowed.
        /// O(log n)
        /// </summary>
        public void Enqueue(T item, double priority)
        {
            lock(_queue)
            {
                SafeNode node = new SafeNode(item);
                if(_queue.Count == _queue.MaxSize)
                {
                    _queue.Resize(_queue.MaxSize*2 + 1);
                }
                _queue.Enqueue(node, priority);
            }
        }

        /// <summary>
        /// Removes an item from the queue.  The item does not need to be the head of the queue.  
        /// If the item is not in the queue, an exception is thrown.  If unsure, check Contains() first.
        /// If multiple copies of the item are enqueued, only the first one is removed. 
        /// O(n)
        /// </summary>
        public void Remove(T node)
        {
            //TODO: Exception
            lock(_queue)
            {
                SafeNode removeMe = _queue.First(o => o.Data.Equals(node));
                _queue.Remove(removeMe);
            }
        }

        /// <summary>
        /// Call this method to change the priority of an item.
        /// Calling this method on a item not in the queue will throw an exception.
        /// If the item is enqueued multiple times, only the first one will be updated.
        /// (If your requirements are complex enough that you need to enqueue the same item multiple times <i>and</i> be able
        /// to update all of them, please wrap your items in a wrapper class so they can be distinguished).
        /// O(n)
        /// </summary>
        public void UpdatePriority(T item, double priority)
        {
            //TODO: Exception
            lock (_queue)
            {
                SafeNode updateMe = _queue.First(o => o.Data.Equals(item));
                _queue.UpdatePriority(updateMe, priority);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_queue)
            {
                return _queue.Select(o => o.Data).ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValidQueue()
        {
            lock(_queue)
            {
                return _queue.IsValidQueue();
            }
        }
    }
}