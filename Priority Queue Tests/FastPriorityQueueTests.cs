using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    internal class FastPriorityQueueTests : SharedFastPriorityQueueTests<FastPriorityQueue<Node<int>,int>>
    {
        protected override FastPriorityQueue<Node<int>,int> CreateQueue()
        {
            return new FastPriorityQueue<Node<int>,int>(100);
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }
    }
}

