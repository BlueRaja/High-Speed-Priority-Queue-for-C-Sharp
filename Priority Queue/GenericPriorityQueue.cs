using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Priority_Queue
{
    /// <summary>
    /// A copy of StablePriorityQueue which also has generic priority-type
    /// </summary>
    /// <typeparam name="TItem">The values in the queue.  Must extend the GenericPriorityQueue class</typeparam>
    /// <typeparam name="TPriority">The priority-type.  Must extend IComparable&lt;TPriority&gt;</typeparam>
    public sealed class GenericPriorityQueue<TItem, TPriority> : IFixedSizePriorityQueue<TItem, TPriority>
        where TItem : GenericPriorityQueueNode<TPriority>
        where TPriority : IComparable<TPriority>
    {
        private int _numNodes;
        private TItem[] _nodes;
        private long _numNodesEverEnqueued;
        private readonly Comparison<TPriority> _comparer;

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="maxNodes">The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)</param>
        public GenericPriorityQueue(int maxNodes) : this(maxNodes, Comparer<TPriority>.Default) { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="maxNodes">The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)</param>
        /// <param name="comparer">The comparer used to compare TPriority values.</param>
        public GenericPriorityQueue(int maxNodes, IComparer<TPriority> comparer) : this(maxNodes, comparer.Compare) { }

        /// <summary>
        /// Instantiate a new Priority Queue
        /// </summary>
        /// <param name="maxNodes">The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)</param>
        /// <param name="comparer">The comparison function to use to compare TPriority values</param>
        public GenericPriorityQueue(int maxNodes, Comparison<TPriority> comparer)
        {
#if DEBUG
            if (maxNodes <= 0)
            {
                throw new InvalidOperationException("New queue size cannot be smaller than 1");
            }
#endif

            _numNodes = 0;
            _nodes = new TItem[maxNodes + 1];
            _numNodesEverEnqueued = 0;
            _comparer = comparer;
        }

        /// <summary>
        /// Returns the number of nodes in the queue.
        /// O(1)
        /// </summary>
        public int Count
        {
            get
            {
                return _numNodes;
            }
        }

        /// <summary>
        /// Returns the maximum number of items that can be enqueued at once in this queue.  Once you hit this number (ie. once Count == MaxSize),
        /// attempting to enqueue another item will cause undefined behavior.  O(1)
        /// </summary>
        public int MaxSize
        {
            get
            {
                return _nodes.Length - 1;
            }
        }

        /// <summary>
        /// Removes every node from the queue.
        /// O(n) (So, don't do this often!)
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Clear()
        {
            Array.Clear(_nodes, 1, _numNodes);
            _numNodes = 0;
        }

        /// <summary>
        /// Returns (in O(1)!) whether the given node is in the queue.  O(1)
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool Contains(TItem node)
        {
#if DEBUG
            if(node == null)
            {
                throw new ArgumentNullException("node");
            }
            if(node.QueueIndex < 0 || node.QueueIndex >= _nodes.Length)
            {
                throw new InvalidOperationException("node.QueueIndex has been corrupted. Did you change it manually? Or add this node to another queue?");
            }
#endif

            return (_nodes[node.QueueIndex] == node);
        }

        /// <summary>
        /// Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken by first-in-first-out.
        /// If the queue is full, the result is undefined.
        /// If the node is already enqueued, the result is undefined.
        /// O(log n)
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Enqueue(TItem node, TPriority priority)
        {
#if DEBUG
            if(node == null)
            {
                throw new ArgumentNullException("node");
            }
            if(_numNodes >= _nodes.Length - 1)
            {
                throw new InvalidOperationException("Queue is full - node cannot be added: " + node);
            }
            if(Contains(node))
            {
                throw new InvalidOperationException("Node is already enqueued: " + node);
            }
#endif

            node.Priority = priority;
            _numNodes++;
            _nodes[_numNodes] = node;
            node.QueueIndex = _numNodes;
            node.InsertionIndex = _numNodesEverEnqueued++;
            CascadeUp(node);
        }

#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void Swap(TItem node1, TItem node2)
        {
            // Cache to local for faster repeated access
            int index1 = node1.QueueIndex;
            int index2 = node2.QueueIndex;

            //Swap the nodes
            _nodes[index1] = node2;
            _nodes[index2] = node1;

            //Swap their indicies
            node1.QueueIndex = index2;
            node2.QueueIndex = index1;
        }

        //Performance appears to be slightly better when this is NOT inlined o_O
        private void CascadeUp(TItem node)
        {
            //aka Heapify-up
            int nodeIndex = node.QueueIndex;
            int parentIndex = nodeIndex >> 1;
            TPriority nodePriority = node.Priority;
            long nodeInsertionIndex = node.InsertionIndex;
            while(parentIndex >= 1)
            {
                TItem parent = _nodes[parentIndex];
                // Inline to replace repeated property reads with local variable reads
                var cmp = _comparer(parent.Priority, nodePriority);
                if (cmp < 0 || (cmp == 0 && parent.InsertionIndex < nodeInsertionIndex))
                    break;

                //Node has lower priority value, so move parent down the heap to make room
                _nodes[nodeIndex] = parent;
                parent.QueueIndex = nodeIndex;

                nodeIndex = parentIndex;
                parentIndex >>= 1;
            }
            _nodes[nodeIndex] = node;
            node.QueueIndex = nodeIndex;
        }

#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CascadeDown(TItem node)
        {
            //aka Heapify-down
            int finalQueueIndex = node.QueueIndex;
            while(true)
            {
                int childLeftIndex = 2 * finalQueueIndex;

                // If leaf node, we're done
                if (childLeftIndex > _numNodes)
                {
                    node.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = node;
                    break;
                }

                // Check if the left-child is higher-priority than the current node
                int childRightIndex = childLeftIndex + 1;
                TItem childLeft = _nodes[childLeftIndex];
                if (HasHigherPriority(childLeft, node))
                {
                    // Check if there is a right child. If not, swap and finish.
                    if (childRightIndex > _numNodes)
                    {
                        node.QueueIndex = childLeftIndex;
                        childLeft.QueueIndex = finalQueueIndex;
                        _nodes[finalQueueIndex] = childLeft;
                        _nodes[childLeftIndex] = node;
                        break;
                    }
                    // Check if the left-child is higher-priority than the right-child
                    TItem childRight = _nodes[childRightIndex];
                    if (HasHigherPriority(childLeft, childRight))
                    {
                        // left is highest, move it up and continue
                        childLeft.QueueIndex = finalQueueIndex;
                        _nodes[finalQueueIndex] = childLeft;
                        finalQueueIndex = childLeftIndex;
                    }
                    else
                    {
                        // right is even higher, move it up and continue
                        childRight.QueueIndex = finalQueueIndex;
                        _nodes[finalQueueIndex] = childRight;
                        finalQueueIndex = childRightIndex;
                    }
                }
                // Not swapping with left-child, does right-child exist?
                else if (childRightIndex > _numNodes)
                {
                    node.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = node;
                    break;
                }
                else
                {
                    // Check if the right-child is higher-priority than the current node
                    TItem childRight = _nodes[childRightIndex];
                    if (HasHigherPriority(childRight, node))
                    {
                        childRight.QueueIndex = finalQueueIndex;
                        _nodes[finalQueueIndex] = childRight;
                        finalQueueIndex = childRightIndex;
                    }
                    // Neither child is higher-priority than current, so finish and stop.
                    else
                    {
                        node.QueueIndex = finalQueueIndex;
                        _nodes[finalQueueIndex] = node;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if 'higher' has higher priority than 'lower', false otherwise.
        /// Note that calling HasHigherPriority(node, node) (ie. both arguments the same node) will return false
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool HasHigherPriority(TItem higher, TItem lower)
        {
            var cmp = _comparer(higher.Priority, lower.Priority);
            return (cmp < 0 || (cmp == 0 && higher.InsertionIndex < lower.InsertionIndex));
        }

        /// <summary>
        /// Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
        /// If queue is empty, result is undefined
        /// O(log n)
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public TItem Dequeue()
        {
#if DEBUG
            if(_numNodes <= 0)
            {
                throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
            }

            if(!IsValidQueue())
            {
                throw new InvalidOperationException("Queue has been corrupted (Did you update a node priority manually instead of calling UpdatePriority()?" +
                                                    "Or add the same node to two different queues?)");
            }
#endif

            TItem returnMe = _nodes[1];
            //If the node is already the last node, we can remove it immediately
            if (1 == _numNodes)
            {
                _nodes[_numNodes] = null;
                _numNodes = 0;
                return returnMe;
            }

            //Swap the node with the last node
            TItem formerLastNode = _nodes[_numNodes];
            _nodes[1] = formerLastNode;
            formerLastNode.QueueIndex = 1;
            _nodes[_numNodes] = null;
            _numNodes--;

            //Now bubble formerLastNode (which is no longer the last node) down
            CascadeDown(formerLastNode);
            return returnMe;
        }

        /// <summary>
        /// Resize the queue so it can accept more nodes.  All currently enqueued nodes are remain.
        /// Attempting to decrease the queue size to a size too small to hold the existing nodes results in undefined behavior
        /// O(n)
        /// </summary>
        public void Resize(int maxNodes)
        {
#if DEBUG
            if (maxNodes <= 0)
            {
                throw new InvalidOperationException("Queue size cannot be smaller than 1");
            }

            if (maxNodes < _numNodes)
            {
                throw new InvalidOperationException("Called Resize(" + maxNodes + "), but current queue contains " + _numNodes + " nodes");
            }
#endif

            TItem[] newArray = new TItem[maxNodes + 1];
            int highestIndexToCopy = Math.Min(maxNodes, _numNodes);
            Array.Copy(_nodes, newArray, highestIndexToCopy + 1);
            _nodes = newArray;
        }

        /// <summary>
        /// Returns the head of the queue, without removing it (use Dequeue() for that).
        /// If the queue is empty, behavior is undefined.
        /// O(1)
        /// </summary>
        public TItem First
        {
            get
            {
#if DEBUG
                if(_numNodes <= 0)
                {
                    throw new InvalidOperationException("Cannot call .First on an empty queue");
                }
#endif

                return _nodes[1];
            }
        }

        /// <summary>
        /// This method must be called on a node every time its priority changes while it is in the queue.  
        /// <b>Forgetting to call this method will result in a corrupted queue!</b>
        /// Calling this method on a node not in the queue results in undefined behavior
        /// O(log n)
        /// </summary>
#if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void UpdatePriority(TItem node, TPriority priority)
        {
#if DEBUG
            if(node == null)
            {
                throw new ArgumentNullException("node");
            }
            if(!Contains(node))
            {
                throw new InvalidOperationException("Cannot call UpdatePriority() on a node which is not enqueued: " + node);
            }
#endif

            node.Priority = priority;
            OnNodeUpdated(node);
        }

        private void OnNodeUpdated(TItem node)
        {
            //Bubble the updated node up or down as appropriate
            int parentIndex = node.QueueIndex >> 1;

            if(parentIndex > 0 && HasHigherPriority(node, _nodes[parentIndex]))
            {
                CascadeUp(node);
            }
            else
            {
                //Note that CascadeDown will be called if parentNode == node (that is, node is the root)
                CascadeDown(node);
            }
        }

        /// <summary>
        /// Removes a node from the queue.  The node does not need to be the head of the queue.  
        /// If the node is not in the queue, the result is undefined.  If unsure, check Contains() first
        /// O(log n)
        /// </summary>
        public void Remove(TItem node)
        {
#if DEBUG
            if(node == null)
            {
                throw new ArgumentNullException("node");
            }
            if(!Contains(node))
            {
                throw new InvalidOperationException("Cannot call Remove() on a node which is not enqueued: " + node);
            }
#endif

            //If the node is already the last node, we can remove it immediately
            int index = node.QueueIndex;
            if(index == _numNodes)
            {
                _nodes[_numNodes] = null;
                _numNodes--;
                return;
            }

            //Swap the node with the last node
            TItem formerLastNode = _nodes[_numNodes];
            _nodes[index] = formerLastNode;
            formerLastNode.QueueIndex = index;
            _nodes[_numNodes] = null;
            _numNodes--;

            //Now bubble formerLastNode (which is no longer the last node) up or down as appropriate
            OnNodeUpdated(formerLastNode);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            for(int i = 1; i <= _numNodes; i++)
                yield return _nodes[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// <b>Should not be called in production code.</b>
        /// Checks to make sure the queue is still in a valid state.  Used for testing/debugging the queue.
        /// </summary>
        public bool IsValidQueue()
        {
            for(int i = 1; i < _nodes.Length; i++)
            {
                if(_nodes[i] != null)
                {
                    int childLeftIndex = 2 * i;
                    if(childLeftIndex < _nodes.Length && _nodes[childLeftIndex] != null && HasHigherPriority(_nodes[childLeftIndex], _nodes[i]))
                        return false;

                    int childRightIndex = childLeftIndex + 1;
                    if(childRightIndex < _nodes.Length && _nodes[childRightIndex] != null && HasHigherPriority(_nodes[childRightIndex], _nodes[i]))
                        return false;
                }
            }
            return true;
        }
    }
}
