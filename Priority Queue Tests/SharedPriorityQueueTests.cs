using System;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    public class Node : StablePriorityQueueNode
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

    public abstract class SharedPriorityQueueTests<TQueue> where TQueue : IPriorityQueue<Node, float>
    {
        protected TQueue Queue { get; set; }

        protected abstract TQueue CreateQueue();
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
        public void TestEnqueueWorksWithTwoNodesWithSamePriority()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);

            Enqueue(node11);
            Enqueue(node12);

            Node firstNode = Dequeue();
            Node secondNode = Dequeue();

            //Assert we got the correct nodes, but since the queue might not be stable, the order doesn't matter
            Assert.IsTrue((firstNode == node11 && secondNode == node12) || (firstNode == node12 && secondNode == node11));
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

            Assert.AreEqual(3, Queue.Count);
            Assert.IsTrue(Queue.Contains(node1));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsTrue(Queue.Contains(node3));
            Assert.IsTrue(Queue.Contains(node4));
            Assert.IsFalse(Queue.Contains(node5));

            Assert.AreEqual(node1, Dequeue());
            Assert.AreEqual(node3, Dequeue());
            Assert.AreEqual(node4, Dequeue());

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));
            Assert.IsFalse(Queue.Contains(node4));
            Assert.IsFalse(Queue.Contains(node5));
        }

        [Test]
        public void TestRemoveOneItem()
        {
            Node node = new Node(1);

            Enqueue(node);
            Queue.Remove(node);

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node));
        }

        [Test]
        public void TestRemoveMultipleItems()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Queue.Remove(node3);

            Assert.AreEqual(2, Queue.Count);
            Assert.IsTrue(Queue.Contains(node1));
            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));

            Queue.Remove(node1);

            Assert.AreEqual(1, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsTrue(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));

            Queue.Remove(node2);

            Assert.AreEqual(0, Queue.Count);
            Assert.IsFalse(Queue.Contains(node1));
            Assert.IsFalse(Queue.Contains(node2));
            Assert.IsFalse(Queue.Contains(node3));
        }

        [Test]
        public void TestContainsWorksOnNonResetNodeForSameQueue()
        {
            Node node = new Node(1);
            Assert.IsFalse(Queue.Contains(node));

            Enqueue(node);
            Assert.IsTrue(Queue.Contains(node));

            Dequeue();
            Assert.IsFalse(Queue.Contains(node));
        }
    }
}

