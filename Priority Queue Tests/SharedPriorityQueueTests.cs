using System;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    public class Node<K> : StablePriorityQueueNode<K>
    {
        public Node(K priority)
        {
            Priority = priority;
        }

        public override string ToString()
        {
            return String.Format("Priority: {0}, InsertionIndex: {1}, QueueIndex: {2}", Priority, InsertionIndex, QueueIndex);
        }
    }

    public abstract class SharedPriorityQueueTests<TQueue> where TQueue : IPriorityQueue<Node<int>, int>
    {
        protected TQueue Queue { get; set; }

        protected abstract TQueue CreateQueue();
        protected abstract bool IsValidQueue();

        protected void Enqueue(Node<int> node)
        {
            Queue.Enqueue(node, node.Priority);
            Assert.IsTrue(IsValidQueue());
        }

        protected Node<int> Dequeue()
        {
            Node<int> returnMe = Queue.Dequeue();
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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Assert.AreEqual(node1, node1);
            Assert.AreEqual(node2, node2);
            Assert.AreNotEqual(node1, node2);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(0, Queue.Count);

            Enqueue(new Node<int>(1));
            Assert.AreEqual(1, Queue.Count);

            Dequeue();
            Assert.AreEqual(0, Queue.Count);
        }

        [Test]
        public void TestFirst()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            Enqueue(node1);
            Enqueue(node2);

            Assert.AreEqual(node1, Queue.First);
            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node2, Queue.First);
        }

        [Test]
        public void TestEnqueueWorksWithTwoNodesWithSamePriority()
        {
            var node11 = new Node<int>(1);
            var node12 = new Node<int>(1);

            Enqueue(node11);
            Enqueue(node12);

            Node<int> firstNode = Dequeue();
            Node<int> secondNode = Dequeue();

            //Assert we got the correct nodes, but since the queue might not be stable, the order doesn't matter
            Assert.IsTrue((firstNode == node11 && secondNode == node12) || (firstNode == node12 && secondNode == node11));
        }

        [Test]
        public void TestSimpleQueue()
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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

            Node<int> node6 = new Node<int>(6);
            Node<int> node7 = new Node<int>(7);
            Node<int> node8 = new Node<int>(8);
            Node<int> node9 = new Node<int>(9);
            Node<int> node10 = new Node<int>(10);

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
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);
            Node<int> node3 = new Node<int>(3);
            Node<int> node4 = new Node<int>(4);
            Node<int> node5 = new Node<int>(5);

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
    }
}

