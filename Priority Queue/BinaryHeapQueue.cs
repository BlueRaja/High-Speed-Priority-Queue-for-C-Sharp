using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Priority_Queue
{
    public sealed class BinaryHeapQueue<T> : IPriorityQueue<T>
        where T : class, IPriorityQueueNode
    {
        private int _numNodes;
        private readonly T[] _nodes;
        private long _numNodesEverEnqueued;

        public BinaryHeapQueue(int maxNodes)
        {
            _numNodes = 0;
            _nodes = new T[maxNodes];
            _numNodesEverEnqueued = 0;
        }

        /// <summary>
        /// Returns the number of nodes in the queue
        /// </summary>
        public int Count
        {
            get
            {
                return _numNodes;
            }
        }

        /// <summary>
        /// Removes every node from the queue
        /// </summary>
        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        public void Clear()
        {
            for(int i = 0; i < _nodes.Length; i++)
                _nodes[i] = null;
            _numNodes = 0;
        }

        /// <summary>
        /// Returns (in O(1)!) whether the given node is in the queue
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        public bool Contains(T node)
        {
            return (_nodes[node.QueueIndex] == node);
        }

        /// <summary>
        /// Enqueue a node - priority must be set beforehand
        /// </summary>
        /// <param name="node"></param>
        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        public void Enqueue(T node)
        {
            _nodes[_numNodes] = node;
            node.QueueIndex = _numNodes++;
            node.InsertionIndex = _numNodesEverEnqueued++;
            CascadeUp(_nodes[_numNodes - 1]);

            #if DEBUG
            if (!IsValidQueue())
                throw new ArgumentException("Queue is invalid!");
            #endif
        }

        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        private void Swap(T node1, T node2)
        {
            //Swap the nodes
            _nodes[node1.QueueIndex] = node2;
            _nodes[node2.QueueIndex] = node1;

            //Swap their indicies
            int temp = node1.QueueIndex;
            node1.QueueIndex = node2.QueueIndex;
            node2.QueueIndex = temp;
        }

        //Performance appears to be slightly better when this is NOT inlined o_O
        private void CascadeUp(T node)
        {
            //aka Heapify-up
            int parent = (node.QueueIndex - 1) / 2;
            while(parent >= 0 && node.QueueIndex != 0)
            {
                T parentNode = _nodes[parent];
                if(HasHigherPriority(parentNode, node))
                    break;

                //Node has lower priority value, so move it up the heap
                Swap(node, parentNode);

                parent = (node.QueueIndex - 1) / 2;
            }
        }

        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        private void CascadeDown(T node)
        {
            //aka Heapify-down
            T newParent;
            do
            {
                newParent = node;

                int childLeftIndex = 2 * node.QueueIndex + 1;

                //Check if the left-child is higher-priority than the current node
                if(childLeftIndex < _numNodes)
                {
                    T childLeft = _nodes[childLeftIndex];
                    if(HasHigherPriority(childLeft, newParent))
                    {
                        newParent = childLeft;
                    }

                    //Check if the right-child is higher-priority than either the current node or the left child
                    int childRightIndex = childLeftIndex + 1;
                    if(childRightIndex < _numNodes)
                    {
                        T childRight = _nodes[childRightIndex];
                        if(HasHigherPriority(childRight, newParent))
                        {
                            newParent = childRight;
                        }
                    }

                    //If either of the children has higher (smaller) priority, swap and continue cascading
                    if(newParent != node)
                        Swap(newParent, node);
                }
            } while(newParent != node);
        }

        /// <summary>
        /// Returns true if 'higher' has higher priority than 'lower', false otherwise.
        /// Note that calling HasHigherPriority(node, node) (ie. both arguments the same node) will return false
        /// </summary>
        #if NET_VERSION_4_5
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        private bool HasHigherPriority(T higher, T lower)
        {
            return (higher.Priority < lower.Priority ||
                (higher.Priority == lower.Priority && higher.InsertionIndex < lower.InsertionIndex));
        }

        /// <summary>
        /// Removes the head of the queue (node with highest priority), and returns it
        /// </summary>
        public T Dequeue()
        {
            T returnMe = _nodes[0];
            Remove(returnMe);

            #if DEBUG
            if (!IsValidQueue())
                throw new ArgumentException("Queue is invalid!");
            #endif

            return returnMe;
        }

        /// <summary>
        /// Returns the head of the queue
        /// </summary>
        public T First
        {
            get
            {
                return _nodes[0];
            }
        }

        /// <summary>
        /// Update the nodes position in the queue after its priority has changed
        /// </summary>
        public void UpdatedPriority(T node)
        {
            //Bubble the updated node up or down as appropriate
            int parentIndex = (node.QueueIndex - 1) / 2;
            T parentNode = _nodes[parentIndex];

            if(HasHigherPriority(node, parentNode))
            {
                CascadeUp(node);
            }
            else
            {
                //Note that CascadeDown will be called if parentNode == node (that is, node is the root)
                CascadeDown(node);
            }

            #if DEBUG
            if (!IsValidQueue())
                throw new ArgumentException("Queue is invalid!");
            #endif
        }

        /// <summary>
        /// Removes a node from the queue.  Note that the node does not need to be the head of the queue
        /// </summary>
        public void Remove(T node)
        {
            if(_numNodes <= 1)
            {
                _nodes[0] = null;
                _numNodes = 0;
                return;
            }

            //Make sure the node is the last node in the queue
            bool wasSwapped = false;
            T formerLastNode = _nodes[_numNodes - 1];
            if(node.QueueIndex != _numNodes - 1)
            {
                //Swap the node with the last node
                Swap(node, formerLastNode);
                wasSwapped = true;
            }

            _numNodes--;
            _nodes[node.QueueIndex] = null;

            if(wasSwapped)
            {
                //Now bubble formerLastNode (which is no longer the last node) up or down as appropriate
                UpdatedPriority(formerLastNode);
            }

            #if DEBUG
            if (!IsValidQueue())
                throw new ArgumentException("Queue is invalid!");
            #endif
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < _numNodes; i++)
                yield return _nodes[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //Checks to make sure the queue is still in a valid state.  Used for debugging
        public bool IsValidQueue()
        {
            for(int i = 0; i < _nodes.Length; i++)
            {
                if(_nodes[i] != null)
                {
                    int childLeftIndex = 2 * i + 1;
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