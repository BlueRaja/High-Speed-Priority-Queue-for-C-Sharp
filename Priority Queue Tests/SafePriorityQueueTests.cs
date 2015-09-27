using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    public class SafePriorityQueueTests : SharedPriorityQueueTests<SafePriorityQueue<Node>>
    {
        protected override SafePriorityQueue<Node> CreateQueue()
        {
            return new SafePriorityQueue<Node>();
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }

        [Test]
        public void TestQueueAutomaticallyResizes()
        {
            for(int i = 0; i < 1000; i++)
            {
                Enqueue(new Node(i));
                Assert.AreEqual(i+1, Queue.Count);
            }

            for(int i = 0; i < 1000; i++)
            {
                Node node = Dequeue();
                Assert.AreEqual(i, node.Priority);
            }
        }
    }
}

