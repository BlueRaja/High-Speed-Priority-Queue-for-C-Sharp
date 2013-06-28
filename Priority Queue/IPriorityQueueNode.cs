namespace Priority_Queue
{
    public interface IPriorityQueueNode
    {
        long InsertionIndex { get; set; }
        double Priority { get; }
        int QueueIndex { get; set; }
    }
}
