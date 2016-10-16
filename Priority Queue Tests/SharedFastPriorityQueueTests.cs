using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    internal abstract class SharedFastPriorityQueueTests<TQueue> : SharedPriorityQueueTests<TQueue>
        where TQueue : IFixedSizePriorityQueue<Node<int>, int>
    {
        [Test]
        public void TestMaxNodes()
        {
            Assert.AreEqual(100, Queue.MaxSize);
        }

        [Test]
        public void TestResizeEmptyQueue()
        {
            Queue.Resize(10);
            Assert.AreEqual(0, Queue.Count);
            Assert.AreEqual(10, Queue.MaxSize);

            Queue.Resize(3);
            Assert.AreEqual(0, Queue.Count);
            Assert.AreEqual(3, Queue.MaxSize);
        }

        [Test]
        public void TestResizeCopiesNodes()
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            Queue.Resize(10);
            Assert.AreEqual(2, Queue.Count);
            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(1, Queue.Count);
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestResizeQueueWasFull()
        {
            var nodes = new List<Node<int>>(Queue.MaxSize);
            for (int i = 0; i < Queue.MaxSize; i++)
            {
                var node = new Node<int>(i);
                Enqueue(node);
                nodes.Insert(i, node);
            }

            Queue.Resize(Queue.MaxSize * 5);

            for(int i = 0; i < nodes.Count; i++)
            {
                Assert.AreEqual(100-i, Queue.Count);
                Assert.AreEqual(nodes[i], Dequeue());
            }
        }

        [Test]
        public void TestResizeQueueBecomesFull()
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);
            var node3 = new Node<int>(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Queue.Resize(3);
            Assert.AreEqual(3, Queue.MaxSize);
            Assert.AreEqual(3, Queue.Count);
            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(2, Queue.Count);
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(1, Queue.Count);
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(0, Queue.Count);
        }

        #region Debug build only tests
        #if DEBUG
        [Test]
        public void TestDebugEnqueueThrowsOnFullQueue()
        {
            for(int i = 0; i < Queue.MaxSize; i++)
            {
                Node<int> node = new Node<int>(i);
                Enqueue(node);
            }

            Assert.Throws<InvalidOperationException>(() => Queue.Enqueue(new Node<int>(999), 999));
        }

        [Test]
        public void TestDebugEnqueueThrowsOnAlreadyEnqueuedNode()
        {
            Node<int> node = new Node<int>(1);

            Enqueue(node);

            Assert.Throws<InvalidOperationException>(() => Queue.Enqueue(node, 1));
        }

        [Test]
        public void TestDebugDequeueThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Test]
        public void TestDebugDequeueThrowsOnEmptyQueue2()
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Test]
        public void TestDebugFirstThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Test]
        public void TestDebugFirstThrowsOnEmptyQueue2()
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Test]
        public void TestDebugDequeueThrowsOnCorruptedQueue()
        {
            var node1 = new Node<int>(1);
            var node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            node1.Priority = 3; //Don't ever do this! (use Queue.UpdatePriority(node1, 3) instead)

            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Test]
        public void TestDebugRemoveThrowsOnNodeNotInQueue()
        {
            Node<int> node = new Node<int>(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Test]
        public void TestDebugRemoveThrowsOnNodeNotInQueue2()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node2));
        }

        [Test]
        public void TestDebugRemoveThrowsOnNodeNotInQueue3()
        {
            Node<int> node = new Node<int>(1);

            Enqueue(node);

            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Test]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue()
        {
            Node<int> node = new Node<int>(1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Test]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue2()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node2, 3));
        }

        [Test]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue3()
        {
            Node<int> node = new Node<int>(1);

            Enqueue(node);
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Test]
        public void TestDebugResizeThrowsOn0SizeQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Resize(0));
        }

        [Test]
        public void TestDebugResizeSizeTooSmall()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.Throws<InvalidOperationException>(() => Queue.Resize(2));
        }

        [Test]
        public void TestDebugNullParametersThrow()
        {
            Assert.Throws<ArgumentNullException>(() => Queue.Contains(null));
            Assert.Throws<ArgumentNullException>(() => Queue.Enqueue(null, 1));
            Assert.Throws<ArgumentNullException>(() => Queue.Remove(null));
            Assert.Throws<ArgumentNullException>(() => Queue.UpdatePriority(null, 1));
        }

        [Test]
        public void TestDebugContainsOutOfBoundsCloseTo0()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = -1;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Test]
        public void TestDebugContainsOutOfBoundsVeryNegative()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = int.MinValue;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Test]
        public void TestDebugContainsOutOfBoundsAboveMaxSize()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = Queue.MaxSize + 1;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Test]
        public void TestDebugContainsOutOfBoundsVeryLarge()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = int.MaxValue;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }
        #endif
        #endregion
    }
}

