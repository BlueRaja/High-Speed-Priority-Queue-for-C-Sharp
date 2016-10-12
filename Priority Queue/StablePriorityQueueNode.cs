namespace Priority_Queue
{
    public class StablePriorityQueueNode<K> : FastPriorityQueueNode<K>
    {
        /// <summary>
        /// Represents the order the node was inserted in
        /// </summary>
        public long InsertionIndex { get; internal set; }
    }
}
