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
                Assert.AreEqual(i + 1, Queue.Count);
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

        [Test]
        public void TestFirstThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Test]
        public void TestFirstThrowsOnEmptyQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Test]
        public void TestEnqueueRemovesOneCopyOfItem()
        {
            Node node = new Node(1);

            Enqueue(node);
            Enqueue(node);

            Assert.AreEqual(2, Queue.Count);
            Assert.IsTrue(Queue.Contains(node));

            Queue.Remove(node);

            Assert.AreEqual(1, Queue.Count);
            Assert.IsTrue(Queue.Contains(node));

            Queue.Remove(node);

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node));
        }

        [Test]
        public void TestEnqueueRemovesFirstCopyOfItem()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);

            Enqueue(node11);
            Enqueue(node12);
            Enqueue(node11);

            Assert.AreEqual(node11, Queue.First);

            Queue.Remove(node11);

            Assert.AreEqual(node12, Dequeue());
            Assert.AreEqual(node11, Dequeue());
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestMultipleCopiesOfSameItem()
        {
            Node node1 = new Node(1);
            Node node21 = new Node(2);
            Node node22 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node21);
            Enqueue(node22);
            Enqueue(node21);
            Enqueue(node22);
            Enqueue(node3);
            Enqueue(node3);
            Enqueue(node1);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node21, Dequeue());
            Assert.AreEqual(node22, Dequeue());
            Assert.AreEqual(node21, Dequeue());
            Assert.AreEqual(node22, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node3, Dequeue());
        }

        [Test]
        public void TestEnqueuingNull()
        {
            Queue.Enqueue(null, 1);
            Assert.AreEqual(1, Queue.Count);
            Assert.AreEqual(null, Queue.First);
            Assert.IsTrue(Queue.Contains(null));
            Assert.IsFalse(Queue.Contains(new Node(1)));

            Assert.AreEqual(null, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(null));
        }

        [Test]
        public void TestRemoveThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }
    }
}