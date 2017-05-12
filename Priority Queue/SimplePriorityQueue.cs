using System;
using System.Collections;
using System.Collections.Generic;

namespace Priority_Queue
{
    /// <summary>
    /// A simplified priority queue implementation.  Is stable, auto-resizes, and thread-safe, at the cost of being slightly slower than
    /// FastPriorityQueue
    /// </summary>
    /// <typeparam name="TItem">The type to enqueue</typeparam>
    /// <typeparam name="TPriority">The priority-type to use for nodes.  Must extend IComparable&lt;TPriority&gt;</typeparam>
    public class SimplePriorityQueue<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        private class SimpleNode : GenericPriorityQueueNode<TPriority>
        {
            public TItem Data { get; private set; }

            public SimpleNode(TItem data)
            {
                Data = data;
            }
        }

        private const int INITIAL_QUEUE_SIZE = 10;
        private readonly GenericPriorityQueue<SimpleNode, TPriority> _queue;

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        public SimplePriorityQueue() : this(Comparer<TPriority>.Default) { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="comparer">The comparer used to compare TPriority values.  Defaults to Comparer&lt;TPriority&gt;.default</param>
        public SimplePriorityQueue(IComparer<TPriority> comparer) : this(comparer.Compare) { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="comparer">The comparison function to use to compare TPriority values</param>
        public SimplePriorityQueue(Comparison<TPriority> comparer)
        {
            _queue = new GenericPriorityQueue<SimpleNode, TPriority>(INITIAL_QUEUE_SIZE, comparer);
        }

        /// <summary>
        /// Given an item of type T, returns the exist SimpleNode in the queue
        /// </summary>
        private SimpleNode GetExistingNode(TItem item)
        {
            var comparer = EqualityComparer<TItem>.Default;
            foreach(var node in _queue)
            {
                if(comparer.Equals(node.Data, item))
                {
                    return node;
                }
            }
            return null;
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
        public TItem First
        {
            get
            {
                lock(_queue)
                {
                    if(_queue.Count <= 0)
                    {
                        throw new InvalidOperationException("Cannot call .First on an empty queue");
                    }

                    return _queue.First.Data;
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
        public bool Contains(TItem item)
        {
            lock(_queue)
            {
                return GetExistingNode(item) != null;
            }
        }

        /// <summary>
        /// Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
        /// If queue is empty, throws an exception
        /// O(log n)
        /// </summary>
        public TItem Dequeue()
        {
            lock(_queue)
            {
                if(_queue.Count <= 0)
                {
                    throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
                }

                SimpleNode node =_queue.Dequeue();
                return node.Data;
            }
        }

        /// <summary>
        /// Enqueue the item with the given priority, without calling lock(_queue)
        /// </summary>
        private void EnqueueNoLock(TItem item, TPriority priority)
        {
            SimpleNode node = new SimpleNode(item);
            if(_queue.Count == _queue.MaxSize)
            {
                _queue.Resize(_queue.MaxSize * 2 + 1);
            }
            _queue.Enqueue(node, priority);
        }

        /// <summary>
        /// Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken by first-in-first-out.
        /// This queue automatically resizes itself, so there's no concern of the queue becoming 'full'.
        /// Duplicates are allowed.
        /// O(log n)
        /// </summary>
        public void Enqueue(TItem item, TPriority priority)
        {
            lock(_queue)
            {
                EnqueueNoLock(item, priority);
            }
        }

        /// <summary>
        /// Enqueue a node to the priority queue if it doesn't already exist.  Lower values are placed in front. Ties are broken by first-in-first-out.
        /// This queue automatically resizes itself, so there's no concern of the queue becoming 'full'.
        /// Returns true if the node was successfully enqueued; false if it already exists
        /// O(n)
        /// </summary>
        public bool EnqueueWithoutDuplicates(TItem item, TPriority priority)
        {
            lock(_queue)
            {
                if(this.Contains(item))
                {
                    return false;
                }
                EnqueueNoLock(item, priority);
                return true;
            }
        }

        /// <summary>
        /// Removes an item from the queue.  The item does not need to be the head of the queue.  
        /// If the item is not in the queue, an exception is thrown.  If unsure, check Contains() first.
        /// If multiple copies of the item are enqueued, only the first one is removed. 
        /// O(n)
        /// </summary>
        public void Remove(TItem item)
        {
            lock(_queue)
            {
                SimpleNode removeMe = GetExistingNode(item);
                if (removeMe == null)
                {
                    throw new InvalidOperationException("Cannot call Remove() on a node which is not enqueued: " + item);
                }
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
        public void UpdatePriority(TItem item, TPriority priority)
        {
            lock (_queue)
            {
                SimpleNode updateMe = GetExistingNode(item);
                if (updateMe == null)
                {
                    throw new InvalidOperationException("Cannot call UpdatePriority() on a node which is not enqueued: " + item);
                }
                _queue.UpdatePriority(updateMe, priority);
            }
        }

        /// <summary>
        /// Returns the priority of the given item.
        /// Calling this method on a item not in the queue will throw an exception.
        /// If the item is enqueued multiple times, only the priority of the first will be returned.
        /// (If your requirements are complex enough that you need to enqueue the same item multiple times <i>and</i> be able
        /// to query all their priorities, please wrap your items in a wrapper class so they can be distinguished).
        /// O(n) (O(1) if item == queue.First)
        /// </summary>
        public TPriority GetPriority(TItem item)
        {
            lock (_queue)
            {
                SimpleNode findMe = GetExistingNode(item);
                if(findMe == null)
                {
                    throw new InvalidOperationException("Cannot call GetPriority() on a node which is not enqueued: " + item);
                }
                return findMe.Priority;
            }
        }

        #region Try* methods for multithreading
        /// Get the head of the queue, without removing it (use TryDequeue() for that).
        /// Useful for multi-threading, where the queue may become empty between calls to Contains() and First
        /// Returns true if successful, false otherwise
        /// O(1)
        public bool TryFirst(out TItem first)
        {
            lock(_queue)
            {
                if(_queue.Count <= 0)
                {
                    first = default(TItem);
                    return false;
                }

                first = _queue.First.Data;
                return true;
            }
        }

        /// <summary>
        /// Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and sets it to first.
        /// Useful for multi-threading, where the queue may become empty between calls to Contains() and Dequeue()
        /// Returns true if successful; false if queue was empty
        /// O(log n)
        /// </summary>
        public bool TryDequeue(out TItem first)
        {
            lock(_queue)
            {
                if(_queue.Count <= 0)
                {
                    first = default(TItem);
                    return false;
                }

                SimpleNode node = _queue.Dequeue();
                first = node.Data;
                return true;
            }
        }

        /// <summary>
        /// Attempts to remove an item from the queue.  The item does not need to be the head of the queue.  
        /// Useful for multi-threading, where the queue may become empty between calls to Contains() and Remove()
        /// Returns true if the item was successfully removed, false if it wasn't in the queue.
        /// If multiple copies of the item are enqueued, only the first one is removed. 
        /// O(n)
        /// </summary>
        public bool TryRemove(TItem item)
        {
            lock(_queue)
            {
                SimpleNode removeMe = GetExistingNode(item);
                if(removeMe == null)
                {
                    return false;
                }
                _queue.Remove(removeMe);
                return true;
            }
        }

        /// <summary>
        /// Call this method to change the priority of an item.
        /// Useful for multi-threading, where the queue may become empty between calls to Contains() and UpdatePriority()
        /// If the item is enqueued multiple times, only the first one will be updated.
        /// (If your requirements are complex enough that you need to enqueue the same item multiple times <i>and</i> be able
        /// to update all of them, please wrap your items in a wrapper class so they can be distinguished).
        /// Returns true if the item priority was updated, false otherwise.
        /// O(n)
        /// </summary>
        public bool TryUpdatePriority(TItem item, TPriority priority)
        {
            lock(_queue)
            {
                SimpleNode updateMe = GetExistingNode(item);
                if(updateMe == null)
                {
                    return false;
                }
                _queue.UpdatePriority(updateMe, priority);
                return true;
            }
        }

        /// <summary>
        /// Attempt to get the priority of the given item.
        /// Useful for multi-threading, where the queue may become empty between calls to Contains() and GetPriority()
        /// If the item is enqueued multiple times, only the priority of the first will be returned.
        /// (If your requirements are complex enough that you need to enqueue the same item multiple times <i>and</i> be able
        /// to query all their priorities, please wrap your items in a wrapper class so they can be distinguished).
        /// Returns true if the item was found in the queue, false otherwise
        /// O(n) (O(1) if item == queue.First)
        /// </summary>
        public bool TryGetPriority(TItem item, out TPriority priority)
        {
            lock(_queue)
            {
                SimpleNode findMe = GetExistingNode(item);
                if(findMe == null)
                {
                    priority = default(TPriority);
                    return false;
                }
                priority = findMe.Priority;
                return true;
            }
        }
        #endregion

        public IEnumerator<TItem> GetEnumerator()
        {
            List<TItem> queueData = new List<TItem>();
            lock (_queue)
            {
                //Copy to a separate list because we don't want to 'yield return' inside a lock
                foreach(var node in _queue)
                {
                    queueData.Add(node.Data);
                }
            }

            return queueData.GetEnumerator();
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

    /// <summary>
    /// A simplified priority queue implementation.  Is stable, auto-resizes, and thread-safe, at the cost of being slightly slower than
    /// FastPriorityQueue
    /// This class is kept here for backwards compatibility.  It's recommended you use SimplePriorityQueue&lt;TItem, TPriority&gt;
    /// </summary>
    /// <typeparam name="TItem">The type to enqueue</typeparam>
    public class SimplePriorityQueue<TItem> : SimplePriorityQueue<TItem, float>
    {
        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        public SimplePriorityQueue() { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="comparer">The comparer used to compare priority values.  Defaults to Comparer&lt;float&gt;.default</param>
        public SimplePriorityQueue(IComparer<float> comparer) : base(comparer) { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="comparer">The comparison function to use to compare priority values</param>
        public SimplePriorityQueue(Comparison<float> comparer) : base(comparer) { }
    }
}