using System;
using System.Collections.Generic;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    public class SimplePriorityQueueTests : SharedPriorityQueueTests<SimplePriorityQueue<Node>>
    {
        protected override SimplePriorityQueue<Node> CreateQueue()
        {
            return new SimplePriorityQueue<Node>();
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }

        protected void EnqueueWithoutDuplicates(Node node)
        {
            Queue.EnqueueWithoutDuplicates(node, node.Priority);
            Assert.IsTrue(IsValidQueue());
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
        public void TestClearWithDuplicates()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node3);

            Assert.AreEqual(3, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsTrue(Queue.Contains(node3));

            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node3, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));
        }

        [Test]
        public void TestClearWithNull()
        {
            Queue.Enqueue(null, 1);
            Queue.Clear();

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(null));
        }

        [Test]
        public void TestClearWithNullDuplicates()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Queue.Enqueue(null, 3);
            Queue.Enqueue(null, 3);

            Assert.AreEqual(3, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsTrue(Queue.Contains(null));

            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(null, Dequeue());
            Assert.AreEqual(null, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(null));
        }

        [Test]
        public void TestClearWithNullDuplicates2()
        {
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Queue.Enqueue(null, 1);
            Queue.Enqueue(null, 1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node3);

            Assert.AreEqual(3, Queue.Count);
            Assert.IsFalse(Queue.Contains(null));
            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsTrue(Queue.Contains(node3));

            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node3, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(null));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));
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
        public void TestEnqueuingDuplicateNulls()
        {
            Node node = new Node(2);
            Queue.Enqueue(null, 1);
            Queue.Enqueue(node, 2);
            Queue.Enqueue(null, 3);

            Assert.AreEqual(3, Queue.Count);
            Assert.IsTrue(Queue.Contains(null));

            Assert.AreEqual(null, Dequeue());
            Assert.AreEqual(node, Dequeue());

            Assert.IsTrue(Queue.Contains(null));

            Assert.AreEqual(null, Dequeue());
            Assert.AreEqual(0, Queue.Count);

            Assert.IsFalse(Queue.Contains(node));
            Assert.IsFalse(Queue.Contains(null));
        }

        [Test]
        public void TestRemoveThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Test]
        public void TestUpdatePriorityThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Test]
        public void TestCanSortInOppositeDirection()
        {
            Queue = new SimplePriorityQueue<Node>((x, y) => y.CompareTo(x));

            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node1, Dequeue());
        }

        [Test]
        public void TestGetPriority()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.AreEqual(1, Queue.GetPriority(node1));
            Assert.AreEqual(2, Queue.GetPriority(node2));
            Assert.AreEqual(3, Queue.GetPriority(node3));
        }

        [Test]
        public void TestEnqueueWithoutDuplicatesNormal()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            EnqueueWithoutDuplicates(node1);
            EnqueueWithoutDuplicates(node2);
            EnqueueWithoutDuplicates(node3);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
        }

        [Test]
        public void TestEnqueueWithoutDuplicatesWithDuplicates()
        {
            Node node = new Node(1);

            EnqueueWithoutDuplicates(node);
            EnqueueWithoutDuplicates(node);
            EnqueueWithoutDuplicates(node);

            Assert.AreEqual(1, Queue.Count);
            Assert.AreEqual(node, Dequeue());
        }

        [Test]
        public void TestEnqueueWithoutDuplicatesWithDuplicatesMoreComplicated()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);
            Node node2 = new Node(2);

            EnqueueWithoutDuplicates(node11);
            EnqueueWithoutDuplicates(node12);
            EnqueueWithoutDuplicates(node12);
            EnqueueWithoutDuplicates(node2);
            EnqueueWithoutDuplicates(node11);

            Assert.AreEqual(3, Queue.Count);
            Assert.AreEqual(node11, Dequeue());
            Assert.AreEqual(node12, Dequeue());
            Assert.AreEqual(node2, Dequeue());
        }

        [Test]
        public void TestTryFirstEmptyQueue()
        {
            Node first;
            Assert.IsFalse(Queue.TryFirst(out first));
            Assert.IsNull(first);
        }

        [Test]
        public void TestTryFirstEmptyQueueWithPriorityOut()
        {
            Node first;
            float firstPriority;
            Assert.IsFalse(Queue.TryFirst(out first, out firstPriority));
            Assert.IsNull(first);
            Assert.AreEqual(0, firstPriority);
        }

        [Test]
        public void TestTryFirstWithItems()
        {
            Node node = new Node(1);
            Node first;

            Enqueue(node);

            Assert.IsTrue(Queue.TryFirst(out first));
            Assert.AreEqual(node, first);
            Assert.AreEqual(1, Queue.Count);
        }

        [Test]
        public void TestTryFirstWithItemsWithPriorityOut()
        {
            Node node = new Node(1);
            Node first;
            float firstPriority;

            Enqueue(node);

            Assert.IsTrue(Queue.TryFirst(out first, out firstPriority));
            Assert.AreEqual(node, first);
            Assert.AreEqual(1, Queue.Count);
            Assert.AreEqual(node.Priority, firstPriority);
        }

        [Test]
        public void TestTryDequeueEmptyQueue()
        {
            Node first;
            Assert.IsFalse(Queue.TryDequeue(out first));
            Assert.IsNull(first);
        }

        [Test]
        public void TestTryDequeueEmptyQueueWithPriorityOut()
        {
            Node first;
            float firstPriority;
            Assert.IsFalse(Queue.TryDequeue(out first, out firstPriority));
            Assert.IsNull(first);
            Assert.AreEqual(0, firstPriority);
        }

        [Test]
        public void TestTryDequeueWithItems()
        {
            Node node = new Node(1);
            Node first;

            Enqueue(node);

            Assert.IsTrue(Queue.TryDequeue(out first));
            Assert.AreEqual(node, first);
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestTryDequeueWithItemsWithPriorityOut()
        {
            Node node = new Node(1);
            Node first;
            float firstPriority;

            Enqueue(node);

            Assert.IsTrue(Queue.TryDequeue(out first, out firstPriority));
            Assert.AreEqual(node, first);
            Assert.AreEqual(0, Queue.Count);
            Assert.AreEqual(node.Priority, firstPriority);
        }

        [Test]
        public void TestTryRemoveEmptyQueue()
        {
            Node node = new Node(1);

            Assert.IsFalse(Queue.TryRemove(node));
        }

        [Test]
        public void TestTryRemoveEmptyQueueWithPriorityOut()
        {
            Node node = new Node(1);
            float priority;

            Assert.IsFalse(Queue.TryRemove(node, out priority));
            Assert.AreEqual(0, priority);
        }

        [Test]
        public void TestTryRemoveItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsTrue(Queue.TryRemove(node2));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.TryRemove(node2));

            Assert.IsTrue(Queue.Contains(node3));
            Assert.IsTrue(Queue.TryRemove(node3));
            Assert.IsFalse(Queue.Contains(node3));
            Assert.IsFalse(Queue.TryRemove(node3));

            Assert.IsTrue(Queue.Contains(node1));
            Assert.IsTrue(Queue.TryRemove(node1));
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.TryRemove(node1));
        }

        [Test]
        public void TestTryRemoveItemInQueueWithPriorityOut()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            float priority;

            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsTrue(Queue.TryRemove(node2, out priority));
            Assert.AreEqual(node2.Priority, priority);
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.TryRemove(node2, out priority));
            Assert.AreEqual(0, priority);

            Assert.IsTrue(Queue.Contains(node3));
            Assert.IsTrue(Queue.TryRemove(node3, out priority));
            Assert.AreEqual(node3.Priority, priority);
            Assert.IsFalse(Queue.Contains(node3));
            Assert.IsFalse(Queue.TryRemove(node3, out priority));
            Assert.AreEqual(0, priority);

            Assert.IsTrue(Queue.Contains(node1));
            Assert.IsTrue(Queue.TryRemove(node1, out priority));
            Assert.AreEqual(node1.Priority, priority);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.TryRemove(node1, out priority));
            Assert.AreEqual(0, priority);
        }

        [Test]
        public void TestTryRemoveItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);

            Assert.IsFalse(Queue.TryRemove(node3));
        }

        [Test]
        public void TestTryRemoveItemNotInQueueWithPriorityOut()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);

            float priority;

            Assert.IsFalse(Queue.TryRemove(node3, out priority));
            Assert.AreEqual(0, priority);
        }

        [Test]
        public void TestTryUpdatePriorityEmptyQueue()
        {
            Node node = new Node(1);

            Assert.IsFalse(Queue.TryUpdatePriority(node, 2));
        }

        [Test]
        public void TestTryUpdatePriorityItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.IsTrue(Queue.TryUpdatePriority(node2, 0));

            Assert.AreEqual(3, Queue.Count);
            Assert.AreEqual(node2, Dequeue());
        }

        [Test]
        public void TestTryUpdatePriorityItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);

            Assert.IsFalse(Queue.TryUpdatePriority(node3, 0));
        }

        [Test]
        public void TestTryGetPriorityEmptyQueue()
        {
            Node node = new Node(1);
            float priority;

            Assert.IsFalse(Queue.TryGetPriority(node, out priority));
            Assert.AreEqual(0, priority);
        }

        [Test]
        public void TestTryGetPriorityItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            float priority;

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.IsTrue(Queue.TryGetPriority(node2, out priority));
            Assert.AreEqual(2, priority);
        }

        [Test]
        public void TestTryGetPriorityItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            float priority;

            Enqueue(node1);
            Enqueue(node2);

            Assert.IsFalse(Queue.TryGetPriority(node3, out priority));
            Assert.AreEqual(0, priority);
        }
    }
}