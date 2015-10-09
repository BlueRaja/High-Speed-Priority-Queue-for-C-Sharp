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

        [Test]
        public void TestDequeueThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Test]
        public void TestDequeueThrowsOnEmptyQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }
    }
}

