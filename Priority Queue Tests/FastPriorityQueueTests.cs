using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    internal class FastPriorityQueueTests : SharedFastPriorityQueueTests<FastPriorityQueue<Node>>
    {
        protected override FastPriorityQueue<Node> CreateQueue()
        {
            return new FastPriorityQueue<Node>(100);
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }
    }
}

