using System;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    public class Node : PriorityQueueNode
    {
        public Node(int priority)
        {
            Priority = priority;
        }

        public override string ToString()
        {
            return String.Format("Priority: {0}, InsertionIndex: {1}, QueueIndex: {2}", Priority, InsertionIndex, QueueIndex);
        }
    }

    public abstract class SharedPriorityQueueTests<T> where T : IPriorityQueue<Node>
    {
        protected T Queue { get; set; }

        protected abstract T CreateQueue();
        protected abstract bool IsValidQueue();

        protected void Enqueue(Node node)
        {
            Queue.Enqueue(node, node.Priority);
            Assert.IsTrue(IsValidQueue());
        }

        protected Node Dequeue()
        {
            Node returnMe = Queue.Dequeue();
            Assert.IsTrue(IsValidQueue());
            return returnMe;
        }

        [SetUp]
        public void SetUp()
        {
            Queue = CreateQueue();
        }

        [Test]
        public void TestSanity()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Assert.AreEqual(node1, node1);
            Assert.AreEqual(node2, node2);
            Assert.AreNotEqual(node1, node2);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(0, Queue.Count);

            Enqueue(new Node(1));
            Assert.AreEqual(1, Queue.Count);

            Dequeue();
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestFirst()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Assert.AreEqual(node1, Queue.First);
            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Queue.First);
        }

        [Test]
        public void TestFirstIsNullOnEmptyQueue()
        {
            Node node1 = new Node(1);

            Assert.AreEqual(null, Queue.First);

            Enqueue(node1);
            Dequeue();

            Assert.AreEqual(null, Queue.First);
        }

        [Test]
        public void TestEnQueueWorksWithTwoNodesWithSamePriority()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);

            Enqueue(node11);
            Enqueue(node12);

            Assert.AreEqual(node11, Dequeue());
            Assert.AreEqual(node12, Dequeue());
        }

        [Test]
        public void TestSimpleQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());
        }

        [Test]
        public void TestForwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node4);
            Enqueue(node5);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());
        }

        [Test]
        public void TestBackwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node5);
            Enqueue(node4);
            Enqueue(node3);
            Enqueue(node2);
            Enqueue(node1);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());
        }

        [Test]
        public void TestAddingSameNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());

            Enqueue(node5);
            Enqueue(node3);
            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node4);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());
        }

        [Test]
        public void TestAddingDifferentNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());

            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);
            Node node9 = new Node(9);
            Node node10 = new Node(10);

            Enqueue(node6);
            Enqueue(node7);
            Enqueue(node8);
            Enqueue(node10);
            Enqueue(node9);

            Assert.AreEqual(node6, Dequeue());
            Assert.AreEqual(node7, Dequeue());
            Assert.AreEqual(node8, Dequeue());
            Assert.AreEqual(node9, Dequeue());
            Assert.AreEqual(node10, Dequeue());
        }

        [Test]
        public void TestClear()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Queue.Clear();
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestOrderedQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(1);
            Node node3 = new Node(1);
            Node node4 = new Node(1);
            Node node5 = new Node(1);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node4);
            Enqueue(node5);

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());
            Assert.AreEqual(node5, Dequeue());
        }

        [Test]
        public void TestMoreComplicatedQueue()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);
            Node node13 = new Node(1);
            Node node14 = new Node(1);
            Node node15 = new Node(1);
            Node node21 = new Node(2);
            Node node22 = new Node(2);
            Node node23 = new Node(2);
            Node node24 = new Node(2);
            Node node25 = new Node(2);
            Node node31 = new Node(3);
            Node node32 = new Node(3);
            Node node33 = new Node(3);
            Node node34 = new Node(3);
            Node node35 = new Node(3);
            Node node41 = new Node(4);
            Node node42 = new Node(4);
            Node node43 = new Node(4);
            Node node44 = new Node(4);
            Node node45 = new Node(4);
            Node node51 = new Node(5);
            Node node52 = new Node(5);
            Node node53 = new Node(5);
            Node node54 = new Node(5);
            Node node55 = new Node(5);

            Enqueue(node31);
            Enqueue(node51);
            Enqueue(node52);
            Enqueue(node11);
            Enqueue(node21);
            Enqueue(node22);
            Enqueue(node53);
            Enqueue(node41);
            Enqueue(node12);
            Enqueue(node32);
            Enqueue(node13);
            Enqueue(node42);
            Enqueue(node43);
            Enqueue(node44);
            Enqueue(node45);
            Enqueue(node54);
            Enqueue(node14);
            Enqueue(node23);
            Enqueue(node24);
            Enqueue(node33);
            Enqueue(node34);
            Enqueue(node55);
            Enqueue(node35);
            Enqueue(node25);
            Enqueue(node15);

            Assert.AreEqual(node11, Dequeue());
            Assert.AreEqual(node12, Dequeue());
            Assert.AreEqual(node13, Dequeue());
            Assert.AreEqual(node14, Dequeue());
            Assert.AreEqual(node15, Dequeue());
            Assert.AreEqual(node21, Dequeue());
            Assert.AreEqual(node22, Dequeue());
            Assert.AreEqual(node23, Dequeue());
            Assert.AreEqual(node24, Dequeue());
            Assert.AreEqual(node25, Dequeue());
            Assert.AreEqual(node31, Dequeue());
            Assert.AreEqual(node32, Dequeue());
            Assert.AreEqual(node33, Dequeue());
            Assert.AreEqual(node34, Dequeue());
            Assert.AreEqual(node35, Dequeue());
            Assert.AreEqual(node41, Dequeue());
            Assert.AreEqual(node42, Dequeue());
            Assert.AreEqual(node43, Dequeue());
            Assert.AreEqual(node44, Dequeue());
            Assert.AreEqual(node45, Dequeue());
            Assert.AreEqual(node51, Dequeue());
            Assert.AreEqual(node52, Dequeue());
            Assert.AreEqual(node53, Dequeue());
            Assert.AreEqual(node54, Dequeue());
            Assert.AreEqual(node55, Dequeue());
        }
    }
}

