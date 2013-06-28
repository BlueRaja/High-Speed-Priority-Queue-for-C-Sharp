namespace Priority_Queue
{
    public interface IPriorityQueueNode
    {
        /// <summary>
        /// The Priority to insert this node at.  Must be set BEFORE adding a node to the queue
        /// </summary>
        double Priority { get; }

        /// <summary>
        /// <b>Used by the priority queue - do not edit this value.</b>
        /// Represents the order the node was inserted in
        /// </summary>
        long InsertionIndex { get; set; }

        /// <summary>
        /// <b>Used by the priority queue - do not edit this value.</b>
        /// Represents the current position in the queue
        /// </summary>
        int QueueIndex { get; set; }
    }
}
