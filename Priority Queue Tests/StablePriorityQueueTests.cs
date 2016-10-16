using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    internal class StablePriorityQueueTests : SharedFastPriorityQueueTests<StablePriorityQueue<Node<int>,int>>
    {
        protected override StablePriorityQueue<Node<int>,int> CreateQueue()
        {
            return new StablePriorityQueue<Node<int>,int>(100);
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }

        [Test]
        public void TestOrderedQueue()
        {
            SharedStablePriorityQueueTests.TestOrderedQueue(Enqueue, Dequeue);
        }

        [Test]
        public void TestMoreComplicatedOrderedQueue()
        {
            SharedStablePriorityQueueTests.TestMoreComplicatedOrderedQueue(Enqueue, Dequeue);
        }
    }
}

