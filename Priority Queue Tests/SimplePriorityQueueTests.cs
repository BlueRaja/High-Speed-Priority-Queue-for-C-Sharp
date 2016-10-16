using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    public class SimplePriorityQueueTests : SharedPriorityQueueTests<SimplePriorityQueue<Node<int>,int>>
    {
        protected override SimplePriorityQueue<Node<int>,int> CreateQueue()
        {
            return new SimplePriorityQueue<Node<int>,int>();
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

        [Test]
        public void TestQueueAutomaticallyResizes()
        {
            for(int i = 0; i < 1000; i++)
            {
                Enqueue(new Node<int>(i));
                Assert.AreEqual(i + 1, Queue.Count);
            }

            for(int i = 0; i < 1000; i++)
            {
                var node = Dequeue();
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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Test]
        public void TestEnqueueRemovesOneCopyOfItem()
        {
            Node<int> node = new Node<int>(1);

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
            Node<int> node11 = new Node<int>(1);
            Node<int> node12 = new Node<int>(1);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node21 = new Node<int>(2);
            Node<int> node22 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);

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
            Assert.IsFalse(Queue.Contains(new Node<int>(1)));

            Assert.AreEqual(null, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(null));
        }

        [Test]
        public void TestRemoveThrowsOnNodeNotInQueue()
        {
            Node<int> node = new Node<int>(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Test]
        public void TestUpdatePriorityThrowsOnNodeNotInQueue()
        {
            Node<int> node = new Node<int>(1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }
    }
}